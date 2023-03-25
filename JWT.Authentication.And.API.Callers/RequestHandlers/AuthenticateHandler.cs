using Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Authentication.And.API.Callers.RequestHandlers
{
    public  class AuthenticateHandler
    {
        public async Task SendRequestAsync() 
        {
            string tokenEndpoint = "https://localhost:7202/token";
            string clientId = "applesauce";
            string clientSecret = "bananapudding";

            HttpClient httpClient = new HttpClient();

            var tokenRequest = new ClientRequest { ClientId = clientId, ClientSecret = clientSecret };
            var response = await httpClient.PostAsJsonAsync(tokenEndpoint, tokenRequest);

            if (response.IsSuccessStatusCode)
            {
                var requestEndpoint = "https://localhost:7202/api/test/testrequest";
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                if(tokenResponse != null)
                {
                    string accessToken = tokenResponse.access_token;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Now you can use the httpClient instance to make requests to protected endpoints
                    var protectedResponse = await httpClient.GetAsync(requestEndpoint);
                    if (protectedResponse.IsSuccessStatusCode)
                    {
                        var protectedData = await protectedResponse.Content.ReadAsStringAsync();
                        Console.WriteLine("Protected data: " + protectedData);
                    }
                    else
                    {
                        Console.WriteLine("Failed to access protected endpoint. Status code: " + protectedResponse.StatusCode);
                    }
                }
            }
            else
            {
                throw new Exception("Unable to authenticate with API layer. Please ensure HTTP client is set up correctly and JWT is operating.");
            }
        }

        public async Task SendFailureRequest()
        {
            HttpClient httpClient = new HttpClient();

            var requestEndpoint = "https://localhost:7202/api/test/testrequest";

            var protectedResponse = await httpClient.GetAsync(requestEndpoint);

            if (protectedResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("The request was a success.... this should not have happened.");
            }
            else
            {
                Console.WriteLine("Complete failure... so success? Yes.");
                Console.WriteLine($"reason phrase: {protectedResponse.ReasonPhrase}");
            }
        }
    }
}
