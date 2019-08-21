using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.IdentityAuthorization.Options;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace AspNetCore.IdentityAuthorization.Infrastructure
{
    public class DiscoveryInfoProvider
    {

        private readonly IdentityAuthorizationOptions _options;

        private DiscoveryResponse _discoveryInfo;

        public DiscoveryInfoProvider(IOptions<IdentityAuthorizationOptions> options)
        {
            _options = options.Value;

            _discoveryInfo = null;
        }


        public async Task<DiscoveryResponse> GetDiscoveryInfo()
        {
            if (_discoveryInfo != null)
                return _discoveryInfo;


            var client = new HttpClient();

            var response = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _options.Authority,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = _options.RequireHttpsAuthority
                }
            });

            // cache response
            _discoveryInfo = response;

            return response;

        }

    }
}
