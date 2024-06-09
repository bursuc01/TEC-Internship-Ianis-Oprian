using ApiApp.DataAccessLayer.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiApp.BusinessLogicLayer.TokenBLL
{
    public class TokenService : ITokenService
    {
        private IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateTokenOptions(User user)
        {
            var claims = new[]
       {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.Name),
            new Claim("IsAdmin", user.IsAdmin.ToString()),
        };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public string Decrypt(string message, int key)
        {
            var encryptedMessage = new StringBuilder();
            foreach (char c in message)
            {
                encryptedMessage.Append((char)(c - key));
            }
            return encryptedMessage.ToString();
        }
    }
}
