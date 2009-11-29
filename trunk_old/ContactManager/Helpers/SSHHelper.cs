using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;
using ContactManager.Helpers;
using ContactManager.Intefaces;
using Tamir.SharpSsh;
using System.Text;

namespace ContactManager.Helpers
{
    public class SSHHelper : ISSHHelper, IDisposable
    {
        readonly SshStream sshStream;
        readonly ParserHelper pHelper = new ParserHelper();
        readonly AstraEntities _astraEntities = new AstraEntities();

        public SSHHelper()
        {
            var user = new User();
            var host = _astraEntities.HostSet.Where(h => h.HostId == user.HostId).First();
            if (host == null)
                throw new SshTransferException("No Host data saved.");

            try
            {
                sshStream = new SshStream( host.Address, host.UserName,host.UserPassword)
                                {
                                    RemoveTerminalEmulationCharacters = true,
                                    Prompt = "\\[[^@]*@[^]]*]\\s"
                                };
                sshStream.ReadResponse(); //cleaning buffer
            }
            catch (Exception)
            {
                if (sshStream != null) CloseSession();
                throw;
            }
        }
        public void CloseSession()
        {
            sshStream.Flush();
            sshStream.Close();
        }

        public List<User> GetUsers()
        {            
            sshStream.Write("ppp secret print detail without-paging");
            //return pHelper.BuildUsers(sshStream.ReadResponse());
            throw new NotImplementedException();
        }

        public List<Tariff> GetTariffs()
        {
            sshStream.Write("queue simple print detail without-paging");
            return pHelper.BuildTariffs(sshStream.ReadResponse());
        }

        public void UpdateUser(User user, out IValidatorStatus status)
        {
            new ValidatorStatus();
            var helper = new UserHelper();
            status = new ValidatorStatus();
            var aUser = _astraEntities.ASPUserSet.Where(u => u.UserId == user.UserId).First();
            aUser.astra_ClientsReference.Load();
            aUser.astra_ClientsReference.Value.astra_TariffsReference.Load();

            var tariff = aUser.astra_ClientsReference.Value.astra_TariffsReference.Value;
            var client = aUser.astra_ClientsReference.Value;
            var command = "ppp secret set {0} password={1} service={2} local-address={3} remote-address={4} disabled={5}";
            command = String.Format(command,
                                    user.UserName,
                                    helper.GetUserPassword(aUser.UserName),
                                    tariff.Service,
                                    client.LocalAddress,
                                    client.RemoteAddress,
                                    client.Status.Equals("Disabled").ToString().ToLower());
            sshStream.Write(command);
            sshStream.ReadResponse();
            command = "queue simple set {0} target-addresses={1} interface={2} parent={3} direction={4} priority={5} queue={6} limit-at={7} max-limit={8} total-queue={9} disabled={10}";
            command = String.Format(command,
                                    user.UserName,
                                    client.RemoteAddress,
                                    "all",
                                    "none",
                                    tariff.Direction,
                                    tariff.Priority,
                                    tariff.QueueType,
                                    tariff.Limit_At,
                                    tariff.MaxUploadLimit + "/" + tariff.MaxDownloadLimit,
                                    "default-small",
                                    client.Status.Equals("Disabled").ToString().ToLower());
            sshStream.Write(command);
            sshStream.ReadResponse();
        }

        public void CreateUser(User user)
        {
            var helper = new UserHelper();
            var aUser = _astraEntities.ASPUserSet.Where(u => u.UserId == user.UserId).First();
            aUser.astra_ClientsReference.Load();
            aUser.astra_ClientsReference.Value.astra_TariffsReference.Load();
            var command = new StringBuilder("ppp secret add ");
            var tariff = aUser.astra_ClientsReference.Value.astra_TariffsReference.Value;
            var client = aUser.astra_ClientsReference.Value;
            command.Append(BuildCommand("name", user.UserName))
                    .Append(BuildCommand("password", helper.GetUserPassword(aUser.UserName)))
                    .Append(BuildCommand("service", tariff.Service))
                    .Append(BuildCommand("local-address", client.LocalAddress))
                    .Append(BuildCommand("remote-address", client.RemoteAddress))
                    .Append(BuildCommand("disabled", client.Status.Equals("Disabled").ToString().ToLower()));
            
            sshStream.Write(command.ToString());
            sshStream.ReadResponse();
            command = new StringBuilder("queue simple add ");
            command.Append(BuildCommand("name", user.UserName))
                    .Append(BuildCommand("target-addresses", client.RemoteAddress))
                    .Append(BuildCommand("interface", "all"))
                    .Append(BuildCommand("parent", "none"))
                    .Append(BuildCommand("direction", tariff.Direction))
                    .Append(BuildCommand("priority", tariff.Priority.ToString()))
                    .Append(BuildCommand("queue", tariff.QueueType))
                    .Append(BuildCommand("limit-at", tariff.Limit_At))
                    .Append(BuildCommand("max-limit", tariff.MaxUploadLimit + "/" + tariff.MaxDownloadLimit))
                    .Append(BuildCommand("total-queue", "default-small"))
                    .Append(BuildCommand("disabled", client.Status.Equals("Disabled").ToString().ToLower()));
            
            sshStream.Write(command.ToString());
            sshStream.ReadResponse();
        }

        private static string BuildCommand(String key, String value) 
        {
            if (String.IsNullOrEmpty(value)) return String.Empty;
            return String.Format("{0}={1} ", key, value);
        }

        public void Dispose()
        {
            sshStream.Close();
        }
    }
}