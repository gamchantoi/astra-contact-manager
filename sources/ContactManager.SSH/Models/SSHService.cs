using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.SSH.Intefaces;
using ContactManager.SSH.Models;

namespace ContactManager.SSH.Models
{
    public abstract class SSHService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly CurrentContext _ctx;
        private ISSHRepository _repository;

        #region Constructors
        protected SSHService(IValidationDictionary validationDictionary)
            : this(validationDictionary, null)
        { }

        protected SSHService(IValidationDictionary validationDictionary, ISSHRepository repository)
            : this(validationDictionary, repository, false)
        { }

        protected SSHService(IValidationDictionary validationDictionary, bool autoMode)
            : this(validationDictionary, null, autoMode)
        { }

        protected SSHService(IValidationDictionary validationDictionary, ISSHRepository repository, bool autoMode)
        {
            _validationDictionary = validationDictionary;
            _ctx = new CurrentContext();
            Repository = repository ?? new SSHRepository(_ctx.GetCurrentHost());
            AutoMode = autoMode;

        }
        #endregion

        public ISSHRepository Repository
        {
            get
            {
                if (_repository == null)
                    _repository = new SSHRepository(_ctx.GetCurrentHost());

                return _repository;
            }
            private set
            {
                _repository = value;
            }
        }

        public bool AutoMode
        {
            get
            {
                return Repository.AutoMode;
            }
            set
            {
                Repository.AutoMode = value;
            }
        }

        public bool Connect()
        {
            return Connect(_ctx.GetCurrentHost());
        }

        public bool Connect(Host host)
        {
            return Connect(host.Address, host.UserName, host.UserPassword);
        }

        public bool Connect(string host, string username, string password)
        {
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