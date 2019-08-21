using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.IdentityAuthorization.Infrastructure
{
    public class IdentityAuthorizeAttribute: AuthorizeAttribute
    {

        public IdentityAuthorizeAttribute()
        {
        }

    }
}
