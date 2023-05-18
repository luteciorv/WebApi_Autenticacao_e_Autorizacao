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

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        // Extrair as informações do User Identity
        [HttpGet]
        [Route("user-identity")]
        [Authorize]
        public object TokenInfor() => new { HttpContext.User.Identity };

        // (Role) Qualquer usuário autenticado pode acessar
        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public string Authenticated() => "Qualquer usuário autenticado pode ler esta mensagem.";

        // (Role) Apenas usuários convencionais e adminstradores podem acessar
        [HttpGet]
        [Route("usuario")]
        [Authorize(Roles = "Admin, User, SuperAdmin")]
        public string Users() => "Apenas usuários convencionais e administradores podem ler esta mensagem.";

        // (Role) Apenas usuários administradores podem acessar
        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public string Admins() => "Apenas usuários administradores podem ler esta mensagem.";

        // (Role) Apenas usuários super admins podem acessar
        [HttpGet]
        [Route("super-admin")]
        [Authorize(Roles = "SuperAdmin")]
        public string SuperAdmins() => "Apenas usuários super administradores podem ler esta mensagem.";

        // (Policy) Apenas usuários super admins podem acessar
        [HttpGet]
        [Route("super-admin-politica")]
        [Authorize(Policy = "SuperAdmin")]
        public string SuperAdminsPolicy() => "Apenas usuários super administradores podem ler esta mensagem.";
    }

}
