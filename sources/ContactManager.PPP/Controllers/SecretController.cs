using System;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;
using ContactManager.PPP.SSH;

namespace ContactManager.PPP.Controllers
{
    public class SecretController : Controller
    {
        private readonly ISshSecretService _sshSecretService;
        private readonly ISecretService _secretService;
        private readonly CurrentContext _ctx;

        public SecretController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _secretService = new SecretService(validationDictionary);
            _sshSecretService = new SshSecretService(validationDictionary, true);
            _ctx = new CurrentContext();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid id)
        {
            var secret = _secretService.GetPPPSecret(id);
            if (secret == null)
            {
                var user = _ctx.GetUser(id);
                secret = new PPPSecret
                             {
                                 Name = user.UserName,
                                 UserId = id,
                                 Password = user.Password
                             };
            }

            return View(secret);
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(PPPSecret secret)
        {
            var user = _ctx.GetUser(secret.UserId);
            secret.Name = user.UserName;
            secret.Password = user.Password;

            if (_secretService.GetPPPSecret(secret.UserId) == null)
            {
                secret.Client = _ctx.GetClient(secret.UserId);

                if (_secretService.CreatePPPSecret(secret))
                {
                    _sshSecretService.CreatePPPSecret(secret.UserId);
                }
            }
            else
            {
                if (_secretService.EditPPPSecret(secret))
                {
                    _sshSecretService.EditPPPSecret(secret.UserId);
                }
            }
            //if (ModelState.IsValid)
            return RedirectToAction("Edit", "User", new { id = secret.UserId, area = "Users" });
            //TODO: Return nothing for dialog and exception info display
            //return View(secret);
        }

    }
}
