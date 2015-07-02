using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KSystem.Identity
{
    public class KSystemUserManager : UserManager<KSystemUser>
    {
        public KSystemUserManager(IUserStore<KSystemUser> store)
            : base(store)
        {

        }

        public static KSystemUserManager Create(IdentityFactoryOptions<KSystemUserManager> options,
                                           IOwinContext context)
        {
            KSystemUserContext db = context.Get<KSystemUserContext>();
            KSystemUserManager manager = new KSystemUserManager(new UserStore<KSystemUser>(db));
            return manager;
        }
    }
}