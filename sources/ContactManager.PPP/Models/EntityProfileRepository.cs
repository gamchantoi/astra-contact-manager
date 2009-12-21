using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using ContactManager.PPP.Intefaces;

namespace ContactManager.PPP.Models
{
    public class EntityProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        #region IProfileRepository Members

        public Profile CreateProfile(Profile profile)
        {
            profile.LastUpdatedDate = DateTime.Now;
            profile.mkt_IPPools = ObjectContext.PoolSet.Where(p => p.PoolId == profile.PoolId).FirstOrDefault();
            ObjectContext.AddToProfileSet(profile);
            ObjectContext.SaveChanges();
            return profile;
        }

        public void DeleteProfile(int id)
        {
            var profile = ObjectContext.ProfileSet.Where(p => p.ProfileId == id).FirstOrDefault();
            profile.mkt_PPPSecrets.Load();
            var clientsCount = profile.mkt_PPPSecrets.Count;
            if (clientsCount > 0)
            {
                throw new Exception(string.Format("Profile '{0}' is assigned to {1} user(s), and can't be deleted.",
                                                  profile.Name, clientsCount));
            }
            ObjectContext.DeleteObject(profile);
            ObjectContext.SaveChanges();
        }

        public Profile EditProfile(Profile profile)
        {            
            var _profile = ObjectContext.ProfileSet.Where(p => p.ProfileId == profile.ProfileId).FirstOrDefault();
            var cost = _profile.Cost;
            ObjectContext.ApplyPropertyChanges(_profile.EntityKey.EntitySetName, profile);
            if (!profile.Cost.HasValue)
                _profile.Cost = cost;
            _profile.mkt_IPPools = ObjectContext.PoolSet.Where(p => p.PoolId == profile.PoolId).FirstOrDefault();
            _profile.LastUpdatedDate = DateTime.Now;
            ObjectContext.SaveChanges();
            return _profile;
        }

        public Profile GetProfile(int id)
        {
            var profile = ObjectContext.ProfileSet.Where(p => p.ProfileId == id).FirstOrDefault();
            profile.mkt_IPPoolsReference.Load();
            if (profile.mkt_IPPoolsReference.Value != null)
            {
                profile.PoolName = profile.mkt_IPPoolsReference.Value.Name;
                profile.PoolId = profile.mkt_IPPoolsReference.Value.PoolId;
            }
            return profile;
        }

        public Profile GetProfile(string name)
        {
            var profile = ObjectContext.ProfileSet.Where(p => p.Name == name).FirstOrDefault();
            if (profile == null)
                return null;
            profile.mkt_IPPoolsReference.Load();
            if (profile.mkt_IPPoolsReference.Value != null)
            {
                profile.PoolName = profile.mkt_IPPoolsReference.Value.Name;
                profile.PoolId = profile.mkt_IPPoolsReference.Value.PoolId;
            }
            return profile;
        }

        //public Profile GetProfile(string name)
        //{
        //    return ObjectContext.TariffSet.Where(t => t.Name == name).FirstOrDefault();
        //}

        //public Profile GetProfile(Profile tariff)
        //{
        //    var _tariff = ObjectContext.TariffSet.Where(t =>
        //               t.Direction == tariff.Direction &&
        //                   //t.Service == tariff.Service &&
        //               t.Limit_At == tariff.Limit_At &&
        //               t.MaxDownloadLimit == tariff.MaxDownloadLimit &&
        //               t.MaxUploadLimit == tariff.MaxUploadLimit &&
        //               t.Priority == tariff.Priority
        //       );
        //    if (_tariff.Count() <= 0) return null;

        //    var tar = _tariff.FirstOrDefault();
        //    if (tariff.Clients != null)
        //        tar.Clients = tariff.Clients;
        //    return tar;
        //}

        public List<Profile> ListProfiles()
        {
            foreach (var profile in ObjectContext.ProfileSet.ToList())
            {
                profile.mkt_IPPoolsReference.Load();
                if (profile.mkt_IPPoolsReference.Value != null)
                {
                    profile.PoolId = profile.mkt_IPPoolsReference.Value.PoolId;
                    profile.PoolName = profile.mkt_IPPoolsReference.Value.Name;
                }
            }
            return ObjectContext.ProfileSet.ToList();
        }

        public int DeleteUnAssignedProfiles()
        {
            int count = 0;
            //foreach (var tariff in ObjectContext.TariffSet)
            //{
            //    tariff.astra_Clients.Load();
            //    tariff.astra_QueuesSimple.Load();
            //    var clientsCount = tariff.astra_Clients.Count;
            //    var queuesCount = tariff.astra_QueuesSimple.Count;
            //    if (clientsCount <= 0 && queuesCount <= 0)
            //    {
            //        ObjectContext.DeleteObject(tariff);
            //        count++;
            //    }
            //}
            //ObjectContext.SaveChanges();
            return count;
        }

        #endregion
    }
}