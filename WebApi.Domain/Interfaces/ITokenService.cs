using WebApi.Domain.Models;

namespace WebApi.Domain.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
