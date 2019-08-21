using System;
using System.Linq;
using AspNetCore.IdentityAuthorization.Infrastructure;
using AspNetCore.IdentityAuthorization.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.IdentityAuthorization
{
    public abstract class BaseAuthorizationHandler<TRequirement>: AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseAuthorizationHandler(IOptions<IdentityAuthorizationOptions> options, IHttpContextAccessor httpContextAccessor, DiscoveryInfoProvider discoveryInfoProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            DiscoveryInfoProvider = discoveryInfoProvider;

            Options = options.Value;
        }

        protected readonly IdentityAuthorizationOptions Options;
        protected readonly DiscoveryInfoProvider DiscoveryInfoProvider;

        protected string Token
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues header))
                    throw new ArgumentNullException("Cannot find authorization header");


                return GetToken(header);
            }
        }

        private string GetToken(StringValues header)
        {
            return header.ToString().Replace("Bearer ", "");
        }

    }
}
