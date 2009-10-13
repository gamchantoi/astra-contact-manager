﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class ActiveConnections : IActiveConnections
    {

        #region IActiveConnections Members

        public string Name { get; set; }
        public string Service { get; set; }
        public string CallerId { get; set; }
        public string Address { get; set; }
        public string Uptime { get; set; }
        public string Encoding { get; set; }
        public string SessionId { get; set; }
        public string LimitBytesIn { get; set; }
        public string LimitBytesOut { get; set; }

        #endregion
    }
}
