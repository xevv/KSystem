using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KSystem.Identity
{
    public class KSystemUser : IdentityUser
    {
        public string FIO { get; set; }
        public int Org { get; set; }
        public System.TimeSpan CallTimeStart { get; set; }
        public System.TimeSpan CallTimeEnd { get; set; }
        public int NotificationPhone { get; set; }
        public int NotificationEmail { get; set; }
        public int TestInt { get; set; }
    }
}