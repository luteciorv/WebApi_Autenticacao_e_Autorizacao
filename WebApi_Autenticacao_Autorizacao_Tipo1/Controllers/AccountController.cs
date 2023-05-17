using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_Autenticacao_Autorizacao_Tipo1.Models;
using WebApi_Autenticacao_Autorizacao_Tipo1.Services;

namespace WebApi_Autenticacao_Autorizacao_Tipo1.Controllers
{
    [Route("api/conta")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Qualquer um pode acessar
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User model)
        {
            var userService = new UserService();
            var user = userService.Authenticate(model.Username, model.Password);
            if (user is null)
                return BadRequest(new { message = "Usuário ou senha inválido." });

            return Ok(user);
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
