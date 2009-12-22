using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ContactManager.Models;
using ContactManager.SSH.Intefaces;

namespace ContactManager.PPP.SSH
{
    public class SshSecretRepository
    {
        public SshSecretRepository(ISSHRepository repository)
        {
            Repository = repository;
        }

        public ISSHRepository Repository { get; private set; }

        public List<PPPSecret> ppp_secret_print()
        {
            return BuildPPPSecrets(Repository.RunCommand("ppp secret print detail without-paging"));
        }

        public string ppp_secret_add(PPPSecret secret)
        {
            var command = new StringBuilder("ppp secret add ");
            command.Append(Repository.BuildCommand("name", secret.Name))
                .Append(Repository.BuildCommand("password", secret.Password))
                .Append(Repository.BuildCommand("profile", secret.Profile.Name))
                .Append(Repository.BuildCommand("service", secret.Service))
                .Append(Repository.BuildCommand("local-address", secret.LocalAddress))
                .Append(Repository.BuildCommand("remote-address", secret.RemoteAddress))
                .Append(Repository.BuildCommand("comment", secret.Comment))
                .Append(Repository.BuildCommand("disabled", secret.Disabled.ToString().ToLower()));

            return Repository.RunCommand(command.ToString());
        }

        //public string ppp_secret_add_cache(PPPSecret secret)
        //{
        //    _commandCache.Append(BuildCommand("{add name", secret.Name))
        //        .Append(BuildCommand("password", secret.Password))
        //        .Append(BuildCommand("profile", secret.Profile))
        //        .Append(BuildCommand("service", secret.Service))
        //        .Append(BuildCommand("local-address", secret.LocalAddress))
        //        .Append(BuildCommand("remote-address", secret.RemoteAddress))
        //        .Append(BuildCommand("disabled", secret.Status.Equals(1).ToString().ToLower()));
        //    _commandCache.Append("}");
        //    return _commandCache.ToString();
        //}

        //public string ppp_secret_add_cache_commit()
        //{
        //    _sshStream.Write(_commandCache.ToString());
        //    var result = ParseResponse(_sshStream.ReadResponse(), _commandCache.ToString());
        //    _commandCache = new StringBuilder(_commandBegin.ToString());
        //    return result;
        //}

        public string ppp_secret_set(PPPSecret secret)
        {
            var command = new StringBuilder("ppp secret set ");
            command.Append(secret.Name + " ")
                .Append(Repository.BuildCommand("password", secret.Password))
                .Append(Repository.BuildCommand("profile", secret.Profile.Name))
                .Append(Repository.BuildCommand("service", secret.Service))
                .Append(Repository.BuildCommand("local-address", secret.LocalAddress))
                .Append(Repository.BuildCommand("remote-address", secret.RemoteAddress))
                .Append(Repository.BuildCommand("comment", secret.Comment))
                .Append(Repository.BuildCommand("disabled", secret.Disabled.ToString().ToLower()));

            return Repository.RunCommand(command.ToString());
        }

        public bool ppp_secret_remove(string name)
        {
            var command = "ppp secret remove " + name;
            Repository.RunCommand(command);
            return true;
        }

        public List<PPPSecret> BuildPPPSecrets(string ppp)
        {
            var pppSec = new List<PPPSecret>();
            foreach (var val in Regex.Split(ppp, @"\r\n\s?(?:\d+)"))
            {
                var pppS = new PPPSecret();
                var properties = Repository.BuildProperties(val);
                pppS.Name = Repository.GetValue(properties, "name");
                pppS.Password = Repository.GetValue(properties, "password");
                pppS.Profile = new Profile 
                    {Name = Repository.GetValue(properties, "profile")};
                pppS.LocalAddress = Repository.GetValue(properties, "local-address");
                pppS.RemoteAddress = Repository.GetValue(properties, "remote-address");
                pppS.Disabled = bool.Parse(Repository.GetValue(properties, "status"));
                pppS.Comment = Repository.GetValue(properties, "comment");
                if (!string.IsNullOrEmpty(pppS.Name))
                    pppSec.Add(pppS);
            }
            return pppSec;
        }
    }
}
