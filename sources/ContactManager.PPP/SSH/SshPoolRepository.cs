using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ContactManager.Models;
using ContactManager.SSH.Intefaces;
using ContactManager.SSH.Models;

namespace ContactManager.PPP.SSH
{
    public class SshPoolRepository
    {
        public SshPoolRepository()
        {
            Repository = new SSHRepository();
        }

        public SshPoolRepository(ISSHRepository repository)
        {
            Repository = repository;
        }

        public ISSHRepository Repository { get; private set; }

        public string ip_pool_add(Pool pool)
        {
            var command = new StringBuilder("ip pool add ");
            command.Append(Repository.BuildCommand("name", pool.Name))
                .Append(Repository.BuildCommand("ranges", pool.Addresses))
                .Append(Repository.BuildCommand("next-pool", pool.NextPoolName));

            return Repository.RunCommand(command.ToString());
        }

        public string ip_pool_set(Pool pool)
        {
            var command = new StringBuilder("ip pool set ");
            command.Append(pool.Name + " ")
                .Append(Repository.BuildCommand("ranges", pool.Addresses))
                .Append(Repository.BuildCommand("next-pool", pool.NextPoolName));

            return Repository.RunCommand(command.ToString());
        }

        public bool ip_pool_remove(string name)
        {
            var command = "ip pool remove " + name;
            Repository.RunCommand(command);
            return true;
        }

        public List<Pool> ip_pool_print()
        {
            return BuildIPPools(Repository.RunCommand("ip pool print detail without-paging"));
        }

        public List<Pool> BuildIPPools(string pool)
        {
            var pools = new List<Pool>();
            foreach (var val in Regex.Split(pool, "\r\n\r\n"))
            {
                var properties = Repository.BuildProperties(val);
                var name = Repository.GetValue(properties, "name");

                if (name.Contains("(") || name.Contains(")")) continue;

                var _pool = new Pool
                {
                    Name = name,
                    Addresses = Repository.GetValue(properties, "ranges"),
                    NextPoolName = Repository.GetValue(properties, "next-pool")
                };
                if (!string.IsNullOrEmpty(_pool.Name))
                    pools.Add(_pool);
            }
            return pools;
        }
    }
}
