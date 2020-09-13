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
            while (true)
            {
                //var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest { Address = "http://192.168.99.100:9703/auto", Policy = new DiscoveryPolicy { RequireHttps = false } });

                //var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest { Address = "http://192.168.99.100:5000", Policy = new DiscoveryPolicy { RequireHttps = false } });

                //string url = "http://192.168.99.100:9503/auto/connect/token";
                string url = "http://127.0.0.1:9703/auto/connect/token";

                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    //Address = disco.TokenEndpoint,
                    Address = url,
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

                var response = await client.GetAsync("http://127.0.0.1:9703/basic/api/Home");
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
}
