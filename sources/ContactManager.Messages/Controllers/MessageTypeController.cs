using System.Web.Mvc;
using ContactManager.Messages.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Messages.Controllers
{
    public class MessageTypeController : Controller
    {
         private readonly MessageTypeService _messageTypeService;

        public MessageTypeController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _messageTypeService = new MessageTypeService(validationDictionary);
        }
        public ActionResult Index()
        {
            return View(_messageTypeService.ListMessagesTypes());
        }



        public ActionResult Delete(int id)
        {
            return View();
        }



        public ActionResult Create()
        {
            return View();
        } 



        [HttpPost]
        public ActionResult Create(MessageType messageType)
        {

            if (_messageTypeService.CreateMessageType(messageType))
                return RedirectToAction("Index");
            return View(messageType);
        }


 
        public ActionResult Edit(int id)
        {
            
            return View(_messageTypeService.GetMessageType(id));
        }



        [HttpPost]
        public ActionResult Edit(MessageType messageType)
        {
            if(_messageTypeService.EditMessageType(messageType))
                return RedirectToAction("Index");
            return View(messageType);
            
        }
    }
}
