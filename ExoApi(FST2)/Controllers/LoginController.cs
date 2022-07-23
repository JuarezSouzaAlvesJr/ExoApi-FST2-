using ExoApi_FST2_.Interfaces;
using ExoApi_FST2_.Models;
using ExoApi_FST2_.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExoApi_FST2_.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public LoginController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel dados)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.Login(dados.Email, dados.Senha);

                if (usuarioEncontrado == null)
                {
                    return Unauthorized(new { msg = "Email e/ou senha incorretos" });
                }

                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioEncontrado.Tipo)
                };

                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao"));

                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                //criação do toKen
                var meuToken = new JwtSecurityToken(
                    issuer: "exoapi.webapi",
                    audience: "exoapi.webapi",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credenciais
                    );

                return Ok(
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                        });
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}