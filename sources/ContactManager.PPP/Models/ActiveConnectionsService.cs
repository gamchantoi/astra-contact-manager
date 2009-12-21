using System.Collections.Generic;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.SSH.Models;

namespace ContactManager.PPP.Models
{
    public class ActiveConnectionsService : SSHService, IActiveConnectionsService
    {
        private readonly IActiveConnectionsRepository _activeConnectionRepository;
        //private readonly IValidationDictionary _validationDictionary;

        public ActiveConnectionsService(IValidationDictionary validationDictionary)
            :base (validationDictionary, true)
        {
            // _validationDictionary = validationDictionary;
            _activeConnectionRepository = new ActiveConnectionsRepository(Repository);
        }

        public List<ActiveConnections> ListActiveConnections()
        {
            return _activeConnectionRepository.ppp_active_print();
        }
    }
}
