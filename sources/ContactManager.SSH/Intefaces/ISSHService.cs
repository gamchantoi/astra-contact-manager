﻿using ContactManager.SSH.Intefaces;

namespace ContactManager.SSH.Intefaces
{
    public interface ISSHService
    {
        ISSHRepository Repository { get; }
        bool Connect(string host, string username, string password);
        void Disconnect();

        //bool Connect(string host, string username, string password);
        //bool Connect(Models.Host host);
        //void Disconnect();
        //void CacheBegin();
        //bool CacheCommit();
        //// PPP SECRETS        
        //bool CreatePPPSecret(Guid id);
        //bool EditPPPSecret(Guid id);
        //bool DeletePPPSecret(Guid id);
        //bool DeletePPPSecret(string id);
        //List<PPPSecret> ListPPPSecrets();
        //// PPP PROFILES        
        //bool CreatePPPProfile(int id);
        //bool EditPPPProfile(int id);
        //bool DeletePPPProfile(int id);
        //bool DeletePPPProfile(string name);
        //List<Profile> ListPPPProfiles();
        //// PPP PROFILES        
        //bool CreatePool(int id);
        //bool EditPool(int id);
        //bool DeletePool(int id);
        //bool DeletePool(string name);
        //List<Pool> ListPools();
        //// SYSTEM SCRIPTS
        //string system_script_add(string name, string source);
        //bool system_script_run(string name);
        //// SERVICES
        //bool UpdateServices(PPPSecret secret);
    }
}