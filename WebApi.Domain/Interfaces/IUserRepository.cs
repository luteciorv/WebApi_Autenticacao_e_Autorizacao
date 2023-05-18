using WebApi.Domain.Models;

namespace WebApi.Domain.Interfaces
{
    public interface IUserRepository
    {
        User? Get(string username, string password);
    }
}
