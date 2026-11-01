using
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiJwt.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        [Authorize]
        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            var usuario = User.FindFirst(
                ClaimTypes.Name)?.Value;

            return Ok(new
            {
                mensagem = "Acesso autorizado!",
                usuario = usuario
            });
        }
    }
}