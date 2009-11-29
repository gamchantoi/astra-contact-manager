using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public partial class Profile
    {
        //public Profile() 
        //{
        //    this.mkt_IPPoolsReference.Load();
        //    if(this.mkt_IPPoolsReference.Value != null)
        //        this.PoolName = this.mkt_IPPoolsReference.Value.Name;
        //}
        public int PoolId { get; set; }
        public string PoolName { get; set; }
    }
}
