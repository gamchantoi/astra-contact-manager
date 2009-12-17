using System.Web.Mvc;
using ContactManager.Hosts.Helpers;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Helpers;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;
using ContactManager.PPP.SSH;

namespace ContactManager.PPP.Controllers
{
    public class PoolController : Controller
    {
        private readonly DropDownHelper _ddhelper = new DropDownHelper();
        private readonly HostHelper _hhelper = new HostHelper();
        private readonly IPoolService _service;
        private readonly ISshPoolService _sshService;

        public PoolController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(this.ModelState);
            _service = new PoolService(validationDictionary);
            _sshService = new SshPoolService(validationDictionary, true);
        }

        public ActionResult Index()
        {
            var pools = _service.ListPools();
            pools.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            return View(pools);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewData["Pools"] = _ddhelper.GetPools(null);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "PoolId")] Pool pool)
        {
            if (_service.CreatePool(pool))
            {
                _sshService.CreatePool(pool.PoolId);
                return View("Index", _service.ListPools());
            }
            ViewData["Pools"] = _ddhelper.GetPools(null);
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewData["Pools"] = _ddhelper.GetPools(id);
            return View(_service.GetPool(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Pool pool)
        {
            if (_service.EditPool(pool))
            {
                _sshService.EditPool(pool.PoolId);
                return View("Index", _service.ListPools());
            }
            ViewData["Pools"] = _ddhelper.GetPools(pool.PoolId);
            return View(pool);
        }

        public ActionResult Delete(int id)
        {
            var pool = _service.GetPool(id);
            if (_service.DeletePool(id))
            {
                _sshService.DeletePool(pool.Name);
            }
            
            return View("Index", _service.ListPools());
        }

        public ActionResult DeleteAll()
        {
            foreach (var pool in _service.ListPools())
                Delete(pool.PoolId);

            return View("Index", _service.ListPools());
        }
    }
}