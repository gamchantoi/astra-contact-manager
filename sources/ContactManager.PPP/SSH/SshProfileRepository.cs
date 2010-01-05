using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ContactManager.Models;
using ContactManager.SSH.Intefaces;
using ContactManager.SSH.Models;

namespace ContactManager.PPP.SSH
{
    public class SshProfileRepository
    {
        public SshProfileRepository(bool auto)
        {
            Repository = SSHRepository.Instance;
            Repository.AutoMode = auto;
        }

        public ISSHRepository Repository { get; private set; }

        public string ppp_profile_add(Profile profile)
        {
            var command = new StringBuilder("ppp profile add ");
            command.Append(Repository.BuildCommand("name", profile.Name))
                .Append(Repository.BuildCommand("local-address", profile.LocalAddress))
                .Append(Repository.BuildCommand("remote-address", profile.PoolName))
                .Append(Repository.BuildCommand("rate-limit", profile.RateLimit));

            return Repository.RunCommand(command.ToString());
        }

        public string ppp_profile_set(Profile profile)
        {
            var command = new StringBuilder("ppp profile set ");
            string retVal;
            command.Append((string.IsNullOrEmpty(profile.OldName)? profile.Name : profile.OldName) + " ")
                .Append(Repository.BuildCommand("name", profile.Name))
                .Append(Repository.BuildCommand("local-address", profile.LocalAddress))
                .Append(Repository.BuildCommand("remote-address", profile.PoolName))
                .Append(Repository.BuildCommand("rate-limit", profile.RateLimit));
            retVal = Repository.RunCommand(command.ToString());

            command.Remove(0, command.Length);

            if (string.IsNullOrEmpty(profile.PoolName))
            {
                Repository.RunCommand(string.Format("ppp profile unset {0} remote-address", profile.Name));
            }
            if (string.IsNullOrEmpty(profile.LocalAddress))
            {
                Repository.RunCommand(string.Format("ppp profile unset {0} local-address", profile.Name));
            }
            if (string.IsNullOrEmpty(profile.RateLimit))
            {
                Repository.RunCommand(string.Format("ppp profile unset {0} rate-limit", profile.Name));
            }

            return retVal;
        }

        public bool ppp_profile_remove(string name)
        {
            var command = "ppp profile remove " + name;
            Repository.RunCommand(command);
            return true;
        }

        public List<Profile> ppp_profile_print()
        {
            return BuildProfiles(Repository.RunCommand("ppp profile print detail without-paging"));
        }

        public List<Profile> BuildProfiles(string profiles)
        {
            var _profiles = new List<Profile>();
            foreach (var val in Regex.Split(profiles, "\r\n\r\n"))
            {
                var _profile = new Profile();
                var properties = Repository.BuildProperties(val);
                _profile.Name = Repository.GetValue(properties, "name");
                _profile.LocalAddress = Repository.GetValue(properties, "local-address");
                _profile.PoolName = Repository.GetValue(properties, "remote-address");
                _profile.RateLimit = Repository.GetValue(properties, "rate-limit");
                if (!string.IsNullOrEmpty(_profile.Name))
                    _profiles.Add(_profile);
            }



            //var r = new Regex(PATTERN, RegexOptions.IgnorePatternWhitespace);
            //var r2 = new Regex(@"\x0D\x0A\x20+", RegexOptions.IgnorePatternWhitespace);
            //var str = r2.Split(queue);
            //var safeQueue = new StringBuilder();
            //foreach (var s in str)
            //{
            //    safeQueue.Append(s);
            //}
            //var m = r.Match(safeQueue.ToString());
            //while (m.Success)
            //{
            //    var gk = m.Groups["Key"];
            //    var gv = m.Groups["Value"];
            //    var profile = new Profile();
            //    for (var i = 0; i < gk.Captures.Count; i++)
            //    {
            //        switch (gk.Captures[i].Value)
            //        {
            //            case "name":
            //                profile.Name = gv.Captures[i].Value.Trim();
            //                break;
            //            case "* name":
            //                profile.Name = gv.Captures[i].Value.Trim();
            //                break;
            //            case "local-address":
            //                profile.LocalAddress = gv.Captures[i].Value.Trim();
            //                break;
            //            case "remote-address":
            //                profile.PoolName = gv.Captures[i].Value.Trim();
            //                break;
            //            case "rate-limit":
            //                profile.RateLimit = gv.Captures[i].Value.Trim();
            //                break;
            //            //case "max-limit":
            //            //    var l = gv.Captures[i].Value.Trim().Split(new Char[] { '/' });
            //            //    tariff.MaxUploadLimit = BuildSpeed(l[0].Trim());
            //            //    tariff.MaxDownloadLimit = BuildSpeed(l[1].Trim());
            //            //    break;
            //        }
            //    }
            //    if (!String.IsNullOrEmpty(profile.Name))
            //        profiles.Add(profile);
            //    m = m.NextMatch();
            //}
            return _profiles;
        }
    }
}
