﻿using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Services.Interfaces;
using System.Linq;

namespace ContactManager.Services.Models
{
    public class EntityServiceRepository : BaseRepository, IServiceRepository
    {
        #region Constructors
        public EntityServiceRepository(): this(new AstraEntities())
        {          
        }

        public EntityServiceRepository(AstraEntities entities)
        {
            Entities = entities;
        }
        #endregion

        #region IServiceRepository Members

        public List<Service> ListServices(Statuses? status)
        {
            if (status.HasValue)
            {
                var _status = status.Value == Statuses.Active ? true : false;
                return Entities.ServiceSet.Where(s => s.Active == _status).ToList();
            }
            return Entities.ServiceSet.ToList();
        }

        public Service CreateService(Service service)
        {
            if (service.aspnet_UsersReference.Value == null)
                service.aspnet_Users = Entities.ASPUserSet.Where(u => u.UserId == service.UserId).FirstOrDefault();
            service.LastUpdatedDate = DateTime.Now;
            Entities.AddToServiceSet(service);
            Entities.SaveChanges();
            return service;
        }

        public Service GetService(int id)
        {
            return Entities.ServiceSet.Where(s => s.ServiceId == id).FirstOrDefault();
        }

        public Service BuildService(string systemData)
        {
            var service = Entities.ServiceSet.Where(s => s.SystemData.Equals(systemData) ||
                                                         s.Name.Equals(systemData)).FirstOrDefault();
            if (service == null)
            {
                var _service = new Service
                                   {
                                       Name = systemData,
                                       SystemData = systemData,
                                       IsRegular = true,
                                       Active = true,
                                       Visible = true,
                                       Cost = Decimal.Zero,
                                       aspnet_Users =
                                           Entities.ASPUserSet.Where(u => u.UserName.Equals("System")).FirstOrDefault()
                                   };
                service = CreateService(_service);
            }
            return service;
        }

        public Service EditService(Service service)
        {
            var _service = Entities.ServiceSet.Where(s => s.ServiceId == service.ServiceId).FirstOrDefault();
            if (!string.IsNullOrEmpty(_service.SystemData))
                service.SystemData = _service.SystemData;
            Entities.ApplyPropertyChanges(_service.EntityKey.EntitySetName, service);
            _service.LastUpdatedDate = DateTime.Now;
            Entities.SaveChanges();
            return _service;
        }

        public bool ClientRegistered(Service service)
        {
            return GetActivity(service.ClientId, service.ServiceId) != null;
        }

        public bool RegisterClient(Service service)
        {
            var activity = new ClientInServices
                               {
                                   astra_Services = service,
                                   astra_Clients =
                                       Entities.ClientSet.Where(c => c.UserId == service.ClientId).FirstOrDefault(),
                                   Date = DateTime.Now,
                                   aspnet_Users =
                                       Entities.ASPUserSet.Where(u => u.UserId == service.UserId).FirstOrDefault()
                               };
            Entities.AddToClientInServicesSet(activity);
            Entities.SaveChanges();
            return true;
        }

        public bool UnRegisterClient(Service service)
        {
            var activity = GetActivity(service.ClientId, service.ServiceId);
            //activity.EndDate = DateTime.Now;
            //activity.Active = false;
            Entities.SaveChanges();
            return true;
        }

        private ClientInServices GetActivity(Guid clientId, int serviceId)
        {
            var client = Entities.ClientSet.Where(c => c.UserId == clientId).FirstOrDefault();
            client.astra_ClientsInServices.Load();
            foreach (var item in client.astra_ClientsInServices) 
            {
                item.astra_ServicesReference.Load();
                item.ServiceId = item.astra_ServicesReference.Value.ServiceId;
            }
            return client.astra_ClientsInServices.Where(a => a.ServiceId == serviceId).FirstOrDefault();
        }

        public List<ClientInServices> ListActivities()
        {
            var activities = Entities.ClientInServicesSet.ToList();
            foreach (var item in activities)
            {
                item.astra_ServicesReference.Load();
                item.ServiceId = item.astra_ServicesReference.Value.ServiceId;
                item.ServiceName = item.astra_ServicesReference.Value.Name;

                item.astra_ClientsReference.Load();
                item.ClientId = item.astra_ClientsReference.Value.UserId;
                item.astra_ClientsReference.Value.aspnet_UsersReference.Load();
                item.ClientName = item.astra_ClientsReference.Value.aspnet_UsersReference.Value.UserName;
                
                item.aspnet_UsersReference.Load();
                item.UserId = item.aspnet_UsersReference.Value.UserId;
                item.UserName = item.aspnet_UsersReference.Value.UserName;

            }
            return activities;
        }

        public Service GetService(string name)
        {
            return Entities.ServiceSet.Where(s => s.Name == name).FirstOrDefault();
        }

        #endregion
    }
}