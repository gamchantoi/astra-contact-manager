using System;
using System.Web;
using System.Globalization;
using System.Collections.Generic;
using ContactManager.Intefaces;
using ContactManager.Models;

namespace ContactManager.Models
{
    public class User : IUser, IBaseModel
    {
        public Guid UserId { get; set; }
        public int TariffId { get; set; }
        public int HostId
        {
            get { return int.Parse(HttpContext.Current.Profile.GetPropertyValue("HostId").ToString()); }

            set { HttpContext.Current.Profile.SetPropertyValue("HostId", value); }
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Credit { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string TariffName { get; set; }
        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }

        public bool Validate(out List<ValidatorStatus> status)
        {
            status = new List<ValidatorStatus>();            
            var service = new AccountMembershipService();
            var returnValue = true;
            if (String.IsNullOrEmpty(UserName))
            {
                status.Add(new ValidatorStatus { Key = "UserName", Message = "You must specify a username." });
                returnValue = false;
            }
            //if (String.IsNullOrEmpty(Email))
            //{
            //    status.Add(new ValidatorStatus { Key = "Email", Message = "You must specify an email address." });
            //    returnValue = false;
            //}
            if (Password == null || Password.Length < service.MinPasswordLength)
            {
                status.Add(new ValidatorStatus
                               {
                                   Key = "Password",
                                   Message = String.Format(CultureInfo.CurrentCulture,
                                                           "You must specify a password of {0} or more characters.",
                                                           service.MinPasswordLength)
                               });
                returnValue = false;
            }
            return returnValue;
        }

        public void SetClientFields()
        {
            Role = "client";
            Email = String.Empty;
            Status = "Active";
            Balance = Decimal.Zero;
            LastName = String.Empty;
            FirstName = String.Empty;
            MiddleName = String.Empty;
        }
    }
}