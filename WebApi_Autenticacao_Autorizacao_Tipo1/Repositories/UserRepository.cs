using WebApi_Autenticacao_Autorizacao_Tipo1.Models;

namespace WebApi_Autenticacao_Autorizacao_Tipo1.Repositories
{
    public static class UserRepository
    {
        public static User? Get(string username, string password)
        {
            var users = new List<User>
            {
                new User(1, "usuario", "123", "User"),
                new User(2, "administrador", "123", "Admin")
            };

            return users.Where(u => u.Username.ToLower() == username.ToLower() &&
                                    u.Password.ToLower() == password.ToLower())
                            .FirstOrDefault();
        } 
    }
}