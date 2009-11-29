using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using System;
using ContactManager.Helpers;
using ContactManager.Intefaces;
using log4net;

namespace ContactManager.Helpers
{
    public class SyncHelper : ISyncHelper
    {
        private ILog logger = log4net.LogManager.GetLogger(typeof(_Default));
        #region ISyncHelper Members

        public bool HostToDB(out List<ValidatorStatus> statuses)
        {
            statuses = new List<ValidatorStatus>();
            var sshHelper = new SSHHelper();
            var uHelper = new UserHelper();
            logger.Info("Readeing SSH Users");
            var hUsers = sshHelper.GetUsers();
            logger.Info("Readeing SSH Queues");
            var hTariffs = sshHelper.GetTariffs();
            sshHelper.CloseSession();
            logger.Info("Readeing DB Users");
            var dbUsers = uHelper.GetUsers();
            logger.Info("Saving Users");
            var uStatus = new ValidatorStatus();
            foreach (var user in hUsers)
            {
                if (dbUsers.Exists(u => u.UserName == user.UserName))
                {
                    var dbUser = dbUsers.Find(u => u.UserName == user.UserName);
                    user.UserId = dbUser.UserId;
                    if (dbUser.Role.Trim().Equals("admin")) user.Role = dbUser.Role;
                    user.TariffId = UpdateTariff(hTariffs.Find(t => t.Name == user.UserName));
                    if (!uHelper.UpdateUser(user, out uStatus))
                    {
                        statuses.Add(new ValidatorStatus { Key = "_FORM", Message = user.UserName + ": " + uStatus.Message });
                    }
                }
                else
                {
                    user.TariffId = UpdateTariff(hTariffs.Find(t => t.Name == user.UserName));
                    if (!uHelper.CreateUser(user, out uStatus))
                    {
                        statuses.Add(new ValidatorStatus { Key = "_FORM", Message = user.UserName + ": " + uStatus.Message });
                    }
                }
            }
            logger.Info("Saving complete");
            return true;
        }

        private int UpdateTariff(Tariff tariff)
        {
            if (tariff == null)
                return BaseEntity.Entity.TariffSet.Where(t => t.Name == "Default").First().TariffId;
            var tar = BaseEntity.Entity.TariffSet.Where(t => 
                                                        t.Direction == tariff.Direction && 
                                                        t.Limit_At == tariff.Limit_At &&
                                                        t.MaxDownloadLimit == tariff.MaxDownloadLimit &&
                                                        t.MaxUploadLimit == tariff.MaxUploadLimit &&
                                                        t.Priority == tariff.Priority
                );
            if (tar.Count() > 0)
                return tar.First().TariffId;
            else
            {
                var entity = new BaseEntity();
                tariff.Profile = "default";
                tariff.Service = "any";
                tariff.QueueType = "default-small/default-small";
                tariff.LastUpdatedDate = DateTime.Now;
                tariff.aspnet_Users = entity.CurrentUser;
                tariff.Cost = Decimal.Zero;
                BaseEntity.Entity.AddToTariffSet(tariff);
                BaseEntity.Entity.SaveChanges();
            }
            return tariff.TariffId;
        }

        public bool DBToHost(out List<ValidatorStatus> statuses)
        {
            statuses = new List<ValidatorStatus>();
            var sshHelper = new SSHHelper();
            var uHelper = new UserHelper();
            logger.Info("Getting SSH Users");
            var hUsers = sshHelper.GetUsers();
            logger.Info("Getting DB Users");
            var dbUsers = uHelper.GetUsers();
            logger.Info("Saving Users into Host");
            IValidatorStatus status = new ValidatorStatus();
            foreach (var user in dbUsers)
            {
                if (hUsers.Exists(u => u.UserName == user.UserName))
                {
                    sshHelper.UpdateUser(user, out status);
                    //user.Status = "Updated";
                }
                else
                {
                    sshHelper.CreateUser(user);
                    //user.Status = "Created";
                }
            }
            sshHelper.CloseSession();
            logger.Info("Completing");
            return true;
        }

        #endregion
    }
}