using System;
using AspNetCore.IdentityAuthorization.Handlers;
using AspNetCore.IdentityAuthorization.Infrastructure;
using AspNetCore.IdentityAuthorization.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.IdentityAuthorization.Extensions
{
    public static class DependencyInjection
    {


        public static void AddIdentityAuthorization(this IServiceCollection services, Action<IdentityAuthorizationOptions> options)
        {
            services.Configure(options);

            services.AddSingleton<DiscoveryInfoProvider>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // add handlers as singletons
            services.AddSingleton<IAuthorizationHandler, EmailVerifiedAuthorizationHandler>();


        }


    }
}
