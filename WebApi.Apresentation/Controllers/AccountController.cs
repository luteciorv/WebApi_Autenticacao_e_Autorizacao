using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Commands.Handlers;
using WebApi.Domain.Commands.Requests;
using WebApi.Domain.Commands.Responses;

namespace WebApi.Controllers
{
    [Route("api/conta")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Qualquer um pode acessar
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login(
            [FromServices] IHandler<LoginRequest, LoginResponse> handler,
            [FromBody] LoginRequest request)
        {
            var response = handler.Handle(request); 
            
            if(!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        // Qualquer um autenticado pode acessar
        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public string Authenticated() => "Qualquer usuário autenticado pode ler esta mensagem.";

        // Apenas usuários convencionais e adminstradores
        [HttpGet]
        [Route("usuarios")]
        [Authorize(Roles = "Admin, User")]
        public string Users() => "Apenas usuários convencionais e administradores podem ler esta mensagem.";

        // Apenas usuários administradores
        [HttpGet]
        [Route("administradores")]
        [Authorize(Roles = "Admin")]
        public string Admins() => "Apenas usuários administradores podem ler esta mensagem.";
    }
}
