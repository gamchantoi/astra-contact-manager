using System.Collections.Generic;
using System.Text.RegularExpressions;
using ContactManager.PPP.Intefaces;
using ContactManager.SSH.Intefaces;
using ContactManager.SSH.Models;

namespace ContactManager.PPP.Models
{
    public class ActiveConnectionsRepository : IActiveConnectionsRepository
    {
        public ActiveConnectionsRepository(bool auto)
        {
            Repository = SSHRepository.Instance;
            Repository.AutoMode = auto;
        }

        public ISSHRepository Repository { get; private set; }

        public List<ActiveConnections> ppp_active_print()
        {
            return BuildActiveConnections(Repository.RunCommand("ppp active print detail without-paging"));
        }

        private List<ActiveConnections> BuildActiveConnections(string activeConnections)
        {
            var _activeConnections = new List<ActiveConnections>();

            foreach (var val in Regex.Split(activeConnections, "\r\n\r\n"))
            {
                var _activeConnection = new ActiveConnections();
                var properties = Repository.BuildProperties(val);
                _activeConnection.Name = Repository.GetValue(properties, "name");
                _activeConnection.Address = Repository.GetValue(properties, "address");
                _activeConnection.CallerId = Repository.GetValue(properties, "caller-id");
                _activeConnection.Encoding = Repository.GetValue(properties, "encoding");
                _activeConnection.LimitBytesIn = Repository.GetValue(properties, "limit-bytes-in");
                _activeConnection.LimitBytesOut = Repository.GetValue(properties, "limit-bytes-out");
                _activeConnection.Service = Repository.GetValue(properties, "service");
                _activeConnection.Uptime = Repository.GetValue(properties, "uptime");
                _activeConnection.SessionId = Repository.GetValue(properties, "session-id");

                if (!string.IsNullOrEmpty(_activeConnection.Name))
                    _activeConnections.Add(_activeConnection);
            }
            return _activeConnections;
        }
    }
}
