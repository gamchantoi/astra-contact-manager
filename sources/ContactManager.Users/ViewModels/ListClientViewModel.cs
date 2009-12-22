using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Users.ViewModels
{
    public class ListClientViewModel
    {
        public List<ClientViewModel> Clients;
        public int TotalUsers;
        public decimal TotalBalance;
        public bool Deleted;
    }
}
