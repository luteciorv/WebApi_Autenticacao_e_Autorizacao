using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Domain.Interfaces;
using WebApi.Domain.Models;

namespace WebApi.Application.Services
{
    public class TokenService : ITokenService
    {
        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.PrivateKey);         

            var tokenDescriptor = new SecurityTokenDescriptor // Informações do Token
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
          
            return tokenHandler.WriteToken(token);
        }
    }
}
