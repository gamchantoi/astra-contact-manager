using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Users.ViewModels
{
    public class ClientViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProfileName { get; set; }
        public decimal Balance { get; set; }
        public string Role { get; set; }
        public string StatusDisplayName { get; set; }
        public string StatusName { get; set; }

        public string Password { get; set; }
        public decimal Credit { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }

        public PPPSecret PPPSecret { get; set; }
        public ClientDetail ClientDetails { get; set; }
        public List<Service> Services { get; set; }
        public Address Address { get; set; }
        public Contract Contract { get; set; }

        public SelectList Roles { get; set; }
        public SelectList Profiles { get; set; }
        public SelectList Statuses { get; set; }
    }
}
