using WebApi.Domain.Commands.Requests;
using WebApi.Domain.Commands.Responses;
using WebApi.Domain.Interfaces;

namespace WebApi.Domain.Commands.Handlers
{
    public class LoginHandler : IHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public LoginResponse Handle(LoginRequest request)
        {
            // Verificar se os dados do request são válidos
            request.Validate();
            if (!request.IsValid)
                return new LoginResponse(false, request.Errors);

            // Recuperar o usuário
            var user = _userRepository.Get(request.Username, request.Password);
            if (user is null)
                return new LoginResponse(false, "Nome de usuário ou Senha incorretos. Nenhum usuário encontrado");

            // Gerar o Token
            var token = _tokenService.CreateToken(user);
            if (token is null)
                return new LoginResponse(false, "Não foi possível gerar o Token");

            // Retornar sucesso
            return new LoginResponse(user.Id, user.Username, token, true, "Usuário logado com sucesso");
        }
    }
}
