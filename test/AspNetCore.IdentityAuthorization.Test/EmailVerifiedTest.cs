using AspNetCore.IdentityAuthorization.Requirements;
using Xunit;

namespace AspNetCore.IdentityAuthorization.Test
{
    public class EmailVerifiedTest
    {

        [Fact]
        public void TestPolicyName()
        {
            Assert.Equal("IdsEmailVerified", EmailVerifiedRequirement.PolicyName);
        }

    }
}
