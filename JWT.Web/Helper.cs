using Contracts;
using Newtonsoft.Json;

namespace JWT.Web;

public class Helper
{
    public async Task<string> RetrieveAccessTokenAsync(string clientId, string clientSecret, string baseUrl)
    {
        var httpClient = new HttpClient();
        var tokenRequest = new ClientRequest { ClientId = clientId, ClientSecret = clientSecret };
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/token", tokenRequest);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
            return tokenResponse.access_token;
        }
        else
        {
            throw new InvalidOperationException("Failed to obtain access token");
        }
    }
}