
namespace AspNetCore.IdentityAuthorization.Options
{
    public class IdentityAuthorizationOptions
    {

        public string Authority { get; set; }

        public string EmailVerifiedClaim { get; set; }


        public IdentityAuthorizationOptions()
        {
            EmailVerifiedClaim = "email_verified";
        }

    }
}
