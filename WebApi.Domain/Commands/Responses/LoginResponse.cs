namespace WebApi.Domain.Commands.Responses
{
    public class LoginResponse : ResponseBase
    {
        public LoginResponse(bool success, object message)
        {
            Username = string.Empty;
            Token = string.Empty;
            Success = success;
            Message = message;
        }

        public LoginResponse(int id, string username, string token, bool success, string message)
        {
            Id = id;
            Username = username;
            Token = token;
            Success = success;
            Message = message;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
