using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.SSH.Intefaces;
using System.Text;

namespace ContactManager.SSH.Models
{
    public class EntitySSHRepository_old : ISSHRepository
    {
        private SshStream _sshStream;
        private readonly ParserHelper _helper;
        private StringBuilder _commandCache = new StringBuilder();
        private readonly StringBuilder _commandBegin = new StringBuilder();

        public EntitySSHRepository_old()
        {
            _helper = new ParserHelper();
        }

        #region ISSHRepository Members

        public string Connect(string host, string username, string password)
        {
            _sshStream = new SshStream(host, username, password)
                             {
                                 RemoveTerminalEmulationCharacters = true,
                                 Prompt = @"\[[^@]*@[^]]*]\s>\s?$"
                             };
            return _sshStream.ReadResponse();
        }

        public void Disconnect()
        {
            if (_sshStream != null)
                _sshStream.Close();
        }

        public void CacheBegin(string name)
        {
            _commandBegin.Append(name);
            _commandCache = new StringBuilder(name);
        }

   
        #region PPP PROFILE
        public string ppp_profile_add(Profile profile)
        {
            var command = new StringBuilder("ppp profile add ");
            command.Append(BuildCommand("name", profile.Name))
                .Append(BuildCommand("local-address", profile.LocalAddress))
                .Append(BuildCommand("remote-address", profile.PoolName))
                .Append(BuildCommand("rate-limit", profile.RateLimit));

            _sshStream.Write(command.ToString());
            return ParseResponse(_sshStream.ReadResponse(), command.ToString());
        }

        public string ppp_profile_set(Profile profile)
        {
            var command = new StringBuilder("ppp profile set ");
            command.Append(profile.Name + " ")
                .Append(BuildCommand("local-address", profile.LocalAddress))
                .Append(BuildCommand("remote-address", profile.PoolName))
                .Append(BuildCommand("rate-limit", profile.RateLimit));
            _sshStream.Write(command.ToString());

            command.Remove(0, command.Length);

            if (string.IsNullOrEmpty(profile.PoolName))
            {
                _sshStream.ReadResponse();
                _sshStream.Write(string.Format("ppp profile unset {0} remote-address", profile.Name));
            }
            if (string.IsNullOrEmpty(profile.LocalAddress))
            {
                _sshStream.ReadResponse();
                _sshStream.Write(string.Format("ppp profile unset {0} local-address", profile.Name));
            }
            if (string.IsNullOrEmpty(profile.RateLimit))
            {
                _sshStream.ReadResponse();
                _sshStream.Write(string.Format("ppp profile unset {0} rate-limit", profile.Name));
            }

            return ParseResponse(_sshStream.ReadResponse(), command.ToString());
        }

        public bool ppp_profile_remove(string name)
        {
            var command = "ppp profile remove " + name;
            _sshStream.Write(command.ToString());
            ParseResponse(_sshStream.ReadResponse(), command);
            return true;
        }

        public List<Profile> ppp_profile_print()
        {
            _sshStream.Write("ppp profile print detail without-paging");
            var result = _sshStream.ReadResponse();
            return _helper.BuildProfiles(result);
        }
        #endregion

        #region IP POOL
        public string ip_pool_add(Pool pool)
        {
            var command = new StringBuilder("ip pool add ");
            command.Append(BuildCommand("name", pool.Name))
                .Append(BuildCommand("ranges", pool.Addresses))
                .Append(BuildCommand("next-pool", pool.NextPoolName));

            _sshStream.Write(command.ToString());
            return ParseResponse(_sshStream.ReadResponse(), command.ToString());
        }

        public string ip_pool_set(Pool pool)
        {
            var command = new StringBuilder("ip pool set ");
            command.Append(pool.Name + " ")
                .Append(BuildCommand("ranges", pool.Addresses))
                .Append(BuildCommand("next-pool", pool.NextPoolName));

            _sshStream.Write(command.ToString());
            return ParseResponse(_sshStream.ReadResponse(), command.ToString());
        }

        public bool ip_pool_remove(string name)
        {
            var command = "ip pool remove " + name;
            _sshStream.Write(command.ToString());
            ParseResponse(_sshStream.ReadResponse(), command);
            return true;
        }

        public List<Pool> ip_pool_print()
        {
            _sshStream.Write("ip pool print detail without-paging");
            var result = _sshStream.ReadResponse();
            return _helper.BuildIPPools(result);
        }

        #endregion

        #region QUEUE SIMPLE
        public string queue_simple_add(PPPSecret secret)
        {
            secret.mkt_PPPProfilesReference.Load();
            var command = new StringBuilder("queue simple add ");
            command.Append(BuildCommand("name", secret.Name))
                .Append(BuildCommand("target-addresses", secret.DHCPAddress))
                .Append(BuildCommand("max-limit", secret.mkt_PPPProfilesReference.Value.RateLimit));

            _sshStream.Write(command.ToString());
            return ParseResponse(_sshStream.ReadResponse(), command.ToString());
        }

        public string queue_simple_remove(PPPSecret secret)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IP FIREWALL
        public string ip_firewall_nat_add(PPPSecret secret)
        {
            var command = new StringBuilder("ip firewall nat add ");
            command.Append(BuildCommand("chain", "srcnat"))
                .Append(BuildCommand("action", "masquerade"))
                .Append(BuildCommand("src-address", secret.DHCPAddress))
                .Append(BuildCommand("comment", secret.Name));

            _sshStream.Write(command.ToString());
            return ParseResponse(_sshStream.ReadResponse(), command.ToString());
        }

        public string ip_firewall_nat_remove(PPPSecret secret)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region SCRIPT
        public string system_script_add(string name, string source)
        {
            var command = new StringBuilder("system script add ");
            command.Append(BuildCommand("name", name))
                .Append(BuildCommand("source", source));
            _sshStream.Write(command.ToString());
            return ParseResponse(_sshStream.ReadResponse(), command.ToString());
        }

        public bool system_script_run(string name)
        {
            var command = "system script run " + name;
            _sshStream.Write(command);
            var response = ParseResponse(_sshStream.ReadResponse(), command);
            return true;
        }

        #endregion

        public List<ActiveConnections> ListActiveConnections()
        {
            _sshStream.Write("ppp active print detail without-paging");
            var result = _sshStream.ReadResponse();
            return _helper.BuildActiveConnections(result);
        }

        #endregion

        private static string BuildCommand(String key, String value)
        {
            if (String.IsNullOrEmpty(value)) return String.Empty;
            var builder = new StringBuilder(value);
            builder.Replace("\"", "\\\"")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace("[", "\\[")
                .Replace("]", "\\]");
            return String.Format("{0}=\"{1}\" ", key, builder.ToString().Trim());
        }

        private string ParseResponse(string response, string command)
        {
            string error;
            if (!IsSuccess(response, out error))
                throw new Exception(" Command: '" + command + "' - " + error);
            return response;
        }

        private bool IsSuccess(string result, out string error)
        {
            error = "";
            if (result.Contains("input does not match any value"))
                error = "input does not match any value";
            if (result.Contains("invalid value"))
                error = "invalid value";
            if (result.Contains("invalid item number"))
                error = "invalid item number";
            if (result.Contains("no such command or directory"))
                error = "no such command or directory";
            if (result.Contains("not enough permissions"))
                error = "not enough permissions";
            if(error.Length > 0)
                return false;
            return true;
        }
    }
}