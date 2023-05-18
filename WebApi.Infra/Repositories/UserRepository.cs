using WebApi.Domain.Interfaces;
using WebApi.Domain.Models;

namespace WebApi.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> Users = new();

        public UserRepository()
        {
            Users.Add(new User(1, "usuario", "usuario", "User"));
            Users.Add(new User(2, "administrador", "administrador", "Admin"));
            Users.Add(new User(3, "Luis", "Luis", "SuperAdmin"));
        }

        public User? Get(string username, string password)
        {           
            return Users.Where(u => u.Username.ToLower() == username.ToLower() &&
                                    u.Password.ToLower() == password.ToLower())
                            .FirstOrDefault();
        } 
    }
}