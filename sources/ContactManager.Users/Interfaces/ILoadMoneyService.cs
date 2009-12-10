using System;
using ContactManager.Models.ViewModels;

namespace ContactManager.Users.Interfaces
{
    public interface ILoadMoneyService
    {
        LoadMoneyViewModel GetViewModel(Guid UserId);
        bool LoadMoney(LoadMoneyViewModel model);
    }
}