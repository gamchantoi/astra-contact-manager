using System.Web.Mvc;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;

namespace ContactManager.PPP.Controllers
{
    public class ActiveConnectionsController : Controller
    {
        private readonly IActiveConnectionsService _activeConnectionService;

        public ActiveConnectionsController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _activeConnectionService = new ActiveConnectionsService(validationDictionary);
        }

        public ActionResult Index()
        {
            return View(_activeConnectionService.ListActiveConnections());
        }
    }
}