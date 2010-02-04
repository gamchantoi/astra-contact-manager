using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;

namespace ContactManager.Users.Services
{
    public class ContractService
    {
        private EntityContractRepository _contractRepository;
        private IValidationDictionary _validationDictionary;
        private readonly IUserFacade _userFasade;

        public ContractService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _contractRepository = new EntityContractRepository();
            _userFasade = new UserFacade(_validationDictionary);
        }

        public List<Contract> ListContracts()
        {
            return _contractRepository.ListContracts();
        }

        public void CreateContract(Contract contract)
        {
            var client = _userFasade.ClientService.GetClient(contract.UserId);
            contract.Client.Add(client);
            _contractRepository.CreateContract(contract);
        }

        public Contract GetContract(Guid id)
        {
            return _contractRepository.GetContract(id);
        }

        public Contract EditContract(Contract contract)
        {
            return _contractRepository.EditContract(contract);
        }
    }
}
