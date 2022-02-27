using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MopApp.IdentityServer.MopAppIdentity
{
    public class MopUser : IdentityUser
    {
        public bool IsAdministator { get; set; }
    }
}
