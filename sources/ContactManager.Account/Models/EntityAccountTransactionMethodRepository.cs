using System.Linq;
using ContactManager.Account.Interfaces;
using ContactManager.Models;

namespace ContactManager.Account.Models
{
    public class EntityAccountTransactionMethodRepository : IAccountTransactionMethodRepository
    {
        private readonly AstraEntities _entities;

        #region Constructors
        public EntityAccountTransactionMethodRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityAccountTransactionMethodRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        public AccountTransactionMethod GetServiceMethod()
        {
            return Enumerable.FirstOrDefault(
                       _entities.AccountTransactionMethodSet.Where(m => m.Name == "Service")) 
                   ?? CreateSystemTransactionMethod("Service", "Service fee");
        }

        private AccountTransactionMethod CreateSystemTransactionMethod(string name, string comment)
        {
            var method = new AccountTransactionMethod
                             {
                                 Name = name,
                                 Comment = comment
                             };
            _entities.AddToAccountTransactionMethodSet(method);
            _entities.SaveChanges();
            return method;
        }
    }
}