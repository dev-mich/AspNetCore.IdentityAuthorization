using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.IdentityAuthorization.Requirements
{
    public class EmailVerifiedRequirement: IAuthorizationRequirement
    {

        public const string PolicyName = "IdsEmailVerified";

    }
}
