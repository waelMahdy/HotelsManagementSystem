using HMS.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class HMSRolesManager:RoleManager<IdentityRole>
    {
        public HMSRolesManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        { 
        }
        public static HMSRolesManager Create(IdentityFactoryOptions<HMSRolesManager> options, IOwinContext context)
        {
            return new HMSRolesManager(new RoleStore<IdentityRole>(context.Get<HMSContext>()));
        }
    }
}
