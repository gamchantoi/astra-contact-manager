using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;

namespace ContactManager.Users.Models
{
    public class EntityContractRepository : RepositoryBase<Contract>
    {
        public List<Contract> ListContracts()
        {
            return ObjectContext.ContractSet.ToList();
        }

        public Contract CreateContract(Contract contract)
        {
            ObjectContext.AddToContractSet(contract);
            ObjectContext.SaveChanges();
            return contract;
        }

        public Contract GetContract(Guid id)
        {
            var client =  ObjectContext.Clients.FirstOrDefault(m => m.UserId == id);
            client.ContractReference.Load();
            return client.Contract;
        }

        public Contract EditContract(Contract contract)
        {
            var _contract = ObjectContext.ContractSet.FirstOrDefault(m => m.ContractId == contract.ContractId);
            ObjectContext.ApplyPropertyChanges(_contract.EntityKey.EntitySetName, contract);
            ObjectContext.SaveChanges();
            return contract;
        }
    }
}
