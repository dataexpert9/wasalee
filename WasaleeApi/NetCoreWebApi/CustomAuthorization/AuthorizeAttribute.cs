using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.CustomAuthorization
{
    public class AuthorizeAttribute: Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public override bool Match(object obj)
        {
            var roles = Roles;
            
            return base.Match(obj);
        }
    }
}
