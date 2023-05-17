using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi_Autenticacao_Autorizacao_Tipo1.Models;
using WebApi_Autenticacao_Autorizacao_Tipo1.Repositories;

namespace WebApi_Autenticacao_Autorizacao_Tipo1.Services
{
    public class UserService
    {
        public User? Authenticate(string username, string password)
        {
            // Recuperar o usuário
            var user = UserRepository.Get(username, password);
            if (user is null)
                return null;

            // Criar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.PrivateKey);
            var tokenDescriptor = new SecurityTokenDescriptor // Informaões do Token
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Username),
                    new("Role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Associar os dados do usuário
            user.Token = tokenHandler.WriteToken(token);
            user.Password = string.Empty;

            return user;
        }
    }
}
