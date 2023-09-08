using Microsoft.Extensions.Configuration;

namespace Wata.Commerce.Common.Test
{
    public class IntegrationTestBase
    {
        protected static HttpClient _client;
        public static IConfiguration _configuration;
    }
}