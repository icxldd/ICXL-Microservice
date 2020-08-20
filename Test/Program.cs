using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace Test
{
    class Program
    {
        private static async Task Main()
        {

            var client = new HttpClient();
            
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest { Address = "http://192.168.99.100:5000", Policy = new DiscoveryPolicy { RequireHttps = false } });
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",
                Scope = "api1",

                UserName = "icxl",
                Password = "123456",
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://192.168.99.100:9503/oms/api/values");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
    }
}
