
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiJwt.Models;

namespace ApiJwt.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Usuario != "admin" ||
                login.Senha != "123456")
            {
                return Unauthorized("Usuário ou senha incorretos.");
            }

            var chave = "MinhaChaveSecretaParaTeste123456789";

            var chaveSegura = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave));

            var credenciais = new SigningCredentials(
                chaveSegura,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Usuario)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credenciais
            );

            var tokenGerado = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                mensagem = "Login realizado com sucesso!",
                token = tokenGerado
            });
        }
    }
}
