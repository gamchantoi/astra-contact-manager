using ContactManager.Models;

namespace ContactManager.Account.Interfaces
{
    public interface IAccountTransactionMethodRepository
    {
        AccountTransactionMethod GetServiceMethod();
    }
}