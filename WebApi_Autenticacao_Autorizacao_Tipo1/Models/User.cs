namespace WebApi_Autenticacao_Autorizacao_Tipo1.Models
{
    public class User
    {
        public User(int id, string username, string password, string role)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; set; }
        public string Role { get; private set; }
        public string Token { get; set; } = string.Empty;
    }
}
