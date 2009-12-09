using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContactManager.Models.ViewModels
{
    public class LoadMoneyViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public decimal Balance { get; set; }
        public decimal Sum { get; set; }

        public int MethodId { get; set; }
        public IEnumerable<SelectListItem> LoadMethods { get; set; }
    }
}