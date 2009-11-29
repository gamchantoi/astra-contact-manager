using ContactManager.Account.Interfaces;
using ContactManager.Account.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Account.Models
{
    public class AccountTransactionMethodService : IAccountTransactionMethodService
    {
        private IValidationDictionary _validationDictionary;
        private readonly IAccountTransactionMethodRepository _repository;

        #region Constructors
        public AccountTransactionMethodService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityAccountTransactionMethodRepository())
        { }

        public AccountTransactionMethodService(IValidationDictionary validationDictionary, IAccountTransactionMethodRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }
        #endregion

        public AccountTransactionMethod GetServiceMethod()
        {
            return _repository.GetServiceMethod();
        }
    }
}