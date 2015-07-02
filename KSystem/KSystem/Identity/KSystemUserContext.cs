using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KSystem.Identity
{
    public class KSystemUserContext : IdentityDbContext<KSystemUser>
    {
        public KSystemUserContext() : base("KBaseConnection") { }

        public static KSystemUserContext Create()
        {
            return new KSystemUserContext();
        }
    }
}