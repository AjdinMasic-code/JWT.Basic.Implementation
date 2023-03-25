using Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Helpers
{
    public interface IAuthService
    {
        bool IsUserValid(ClientRequest clientRequest);
        string GenerateJwtToken();
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsUserValid(ClientRequest clientRequest)
        {
            var validUsers = _configuration.GetSection("ValidUsers").GetChildren();
            return validUsers.Any(x => x["ClientId"] == clientRequest.ClientId && x["ClientSecret"] == clientRequest.ClientSecret); 
            //It is best advised to move client info to either a database or Azure Key Vault vs. storing in app due to security. 
            //Or setting up authentication with Azure AD etc. Best used for app to app authentication or a system where this data is stored separately and id and secret are securely generated.
        }

        public string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                null,
                 expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
