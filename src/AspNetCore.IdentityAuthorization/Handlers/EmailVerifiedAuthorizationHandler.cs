using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.IdentityAuthorization.Infrastructure;
using AspNetCore.IdentityAuthorization.Options;
using AspNetCore.IdentityAuthorization.Requirements;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AspNetCore.IdentityAuthorization.Handlers
{
    public class EmailVerifiedAuthorizationHandler: BaseAuthorizationHandler<EmailVerifiedRequirement>
    {
        public EmailVerifiedAuthorizationHandler(IOptions<IdentityAuthorizationOptions> options,
            IHttpContextAccessor httpContextAccessor, DiscoveryInfoProvider discoveryInfoProvider) : base(options,
            httpContextAccessor, discoveryInfoProvider)
        {
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {

            var discoveryInfo = await DiscoveryInfoProvider.GetDiscoveryInfo();

            if (discoveryInfo.Exception != null)
                throw discoveryInfo.Exception;


            // retrieve user info from ids
            var client = new HttpClient();

            var usrInfo = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = discoveryInfo.UserInfoEndpoint,
                Token = Token
            });

            if (usrInfo.Claims == null)
            {
                context.Fail();

                return;
            }

            // retrieve email verified claim, check for claim type in options
            var targetClaim = usrInfo.Claims.FirstOrDefault(c => c.Type == Options.EmailVerifiedClaim);

            if (targetClaim == null)
            {
                context.Fail();

                return;
            }

            // check if claim value is "true" or "True"
            if (targetClaim.Value.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                context.Succeed(requirement);

            else
                context.Fail();

        }
    }
}
