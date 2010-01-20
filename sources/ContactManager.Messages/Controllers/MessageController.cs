using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ContactManager.Messages.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Messages.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageService _messageService;

        public MessageController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _messageService = new MessageService(validationDictionary);
        }

        public ActionResult Index()
        {
            return View(_messageService.ListMessages());
        }
        
        public ActionResult Details(int id)
        {
            return View(_messageService.GetMessage(id));
        }

        public ActionResult Delete(int id)
        {
            return View(_messageService.GetMessage(id));
        }

        [HttpPost]
        public ActionResult Delete(Message message)
        {
            if (_messageService.DeleteMessage(message))
                return RedirectToAction("Index");
            return View(message);
        }

        public ActionResult Create()
        {
            var list = _messageService.MessageTypeService.ListMessagesTypes(null);
            ViewData["List"] = list;
            return View();
        } 
        
        [HttpPost]
        public ActionResult Create(Message message)
        {
            var list = _messageService.MessageTypeService.ListMessagesTypes(null);
            ViewData["List"] = list;
            if (_messageService.CreateMessage(message))
                return RedirectToAction("Index");
            return View(message);
        }
        

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
