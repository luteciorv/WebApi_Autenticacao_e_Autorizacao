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
    }
}
