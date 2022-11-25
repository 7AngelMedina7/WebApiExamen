using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
//        [AllowAnonymous]

        [HttpPost("Registrar")]
        public async Task<ActionResult<Resp>> Registrar(Credenciales credenciales)
        {
            var user = new IdentityUser { UserName = credenciales.Email, Email = credenciales.Email };
            var result = await userManager.CreateAsync(user, credenciales.Contra);

            if (result.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<Resp>> Login(Credenciales credenciales)
        {
            var result = await signInManager.PasswordSignInAsync(credenciales.Email,
                credenciales.Contra, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }


        private async Task<Resp> ConstruirToken(Credenciales credenciales)
        {
            var claims = new List<Claim>
            {
                new Claim("email", credenciales.Email),
                new Claim("segundoclaim", "claimmmmmaa")

            };

            var usuario = await userManager.FindByEmailAsync(credenciales.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new Resp()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration

            };
        }
        [AllowAnonymous]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpPost("Administrador")]
        public async Task<ActionResult> Admin(CrearAdmin admin)
        {
            var aux = await userManager.FindByEmailAsync(admin.Email);

            await userManager.AddClaimAsync(aux, new Claim("EsAdmin", "1"));

            return NoContent();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

        [HttpPost("Inquilino")]
        public async Task<ActionResult> HacerInquilino( HacerInquilino inqui)
        {
            var usuario = await userManager.FindByEmailAsync(inqui.Email);

            await userManager.AddClaimAsync(usuario, new Claim("EsInquilino", "2"));

            return NoContent();
        }

    }
}

