using System.Collections.Generic;

namespace ContactManager.SSH.Intefaces
{
    public interface ISSHRepository
    {
        bool AutoMode { get; set; }

        string Connect(string host, string username, string password);
        void Disconnect();
        string RunCommand(string command);
        string BuildCommand(string key, string value);
        Dictionary<string, string> BuildProperties(string str);
        string GetComment(string str, IDictionary<string, string> kvp);
        string GetValue(IDictionary<string, string> properties, string property);

        //void CacheBegin(string name);
        //List<ActiveConnections> ListActiveConnections();
        ////PPP SECRET
        //string ppp_secret_add(PPPSecret secret);
        //string ppp_secret_add_cache(PPPSecret secret);
        //string ppp_secret_add_cache_commit();
        //string ppp_secret_set(PPPSecret secret);
        //bool ppp_secret_remove(string name);
        //List<PPPSecret> ppp_secret_print();
        ////PPP PROFILE      
        //string ppp_profile_add(Profile profile);
        //string ppp_profile_set(Profile profile);
        //bool ppp_profile_remove(string name);
        //List<Profile> ppp_profile_print();
        ////POOL
        //string ip_pool_add(Pool pool);
        //string ip_pool_set(Pool pool);
        //bool ip_pool_remove(string name);
        //List<Pool> ip_pool_print();
        ////SCRIPT
        //string system_script_add(string name, string source);
        //bool system_script_run(string name);
        ////IP FIREWALL NAT
        //string ip_firewall_nat_add(PPPSecret secret);
        //string ip_firewall_nat_remove(PPPSecret secret);
        ////QUEUE SIMPLE ADD
        //string queue_simple_add(PPPSecret secret);
        //string queue_simple_remove(PPPSecret secret);
    }
}