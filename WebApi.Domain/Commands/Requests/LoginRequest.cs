namespace WebApi.Domain.Commands.Requests
{
    public class LoginRequest : Request
    {      
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;


        public override void Validate()
        {
            if (Username is null || Username.Length == 0 || Username == string.Empty)
                Errors.Add("O campo 'Username' é obrigatório.");

            if (Password is null || Password.Length == 0 || Password == string.Empty)
                Errors.Add("O campo 'Password' é obrigatório.");

            IsValid = Errors.Count == 0;
        }
    }
}
