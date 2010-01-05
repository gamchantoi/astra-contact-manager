using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;
using ContactManager.PPP.SSH;
using ContactManager.SSH.Models;

namespace ContactManager.PPP
{
    public class PPPFactory
    {
        private readonly IValidationDictionary _validationDictionary;

        public PPPFactory(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        public bool SSHAutoMode { get; set; }
        public void SSHConnect()
        {
            SSHRepository.Instance.Connect();
        }

        public void SSHDisconnect() 
        {
            SSHRepository.Instance.Disconnect();
        }

        #region SSH PPP
        public ISshSecretService SSHSecretsService
        {
            get
            {
                return new SshSecretService(_validationDictionary, SSHAutoMode);
            }
        }

        public ISshProfileService SSHProfilesService
        {
            get
            {
                return new SshProfileService(_validationDictionary, SSHAutoMode);
            }
        }

        public ISshPoolService SSHPoolsService
        {
            get
            {
                return new SshPoolService(_validationDictionary, SSHAutoMode);
            }
        }
        #endregion

        #region PPP
        public IPoolService PoolsService
        {
            get
            {
                return new PoolService(_validationDictionary);
            }
        }

        public IProfileService ProfilesService
        {
            get
            {
                return new ProfileService(_validationDictionary);
            }
        }

        public ISecretService SecretService
        {
            get
            {
                return new SecretService(_validationDictionary);
            }
        }
        #endregion
    }
}
