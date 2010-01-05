using System.Collections.Generic;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;

namespace ContactManager.PPP.Services
{
    public class ActiveConnectionsService : IActiveConnectionsService
    {
        private readonly IActiveConnectionsRepository _activeConnectionRepository;
        //private readonly IValidationDictionary _validationDictionary;

        public ActiveConnectionsService(IValidationDictionary validationDictionary)
        {
            // _validationDictionary = validationDictionary;
            _activeConnectionRepository = new ActiveConnectionsRepository(true);
        }

        public List<ActiveConnections> ListActiveConnections()
        {
            return _activeConnectionRepository.ppp_active_print();
        }
    }
}