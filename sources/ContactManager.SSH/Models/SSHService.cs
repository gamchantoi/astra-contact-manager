using ContactManager.Hosts.Helpers;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.SSH.Intefaces;
using ContactManager.SSH.Models;

namespace ContactManager.SSH.Models
{
    public abstract class SSHService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly HostHelper _hostHelper;

        protected SSHService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new SSHRepository())
        {}

        protected SSHService(IValidationDictionary validationDictionary, ISSHRepository repository)
        {
            _validationDictionary = validationDictionary;
            Repository = repository;
            _hostHelper = new HostHelper();

        }

        public ISSHRepository Repository { get; private set; }

        public bool AutoMode { get; set; }

        public bool Connect()
        {
            return Connect(_hostHelper.GetCurrentHost());
        }

        public bool Connect(Host host)
        {
            return Connect(host.Address, host.UserName, host.UserPassword);
        }

        public bool Connect(string host, string username, string password)
        {
            if (Repository == null)
                Repository = new SSHRepository();
            try
            {
                Repository.Connect(host, username, password);
            }
            catch
            {
                _validationDictionary.AddError("_FORM", "SSH connection error.");
                return false;
            }
            return true;
        }

        public void Disconnect()
        {
            Repository.Disconnect();
        }
    }
}