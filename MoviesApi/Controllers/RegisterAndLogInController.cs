using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("Api/RegisterAndLogIn")]
    public class RegisterAndLogInController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public RegisterAndLogInController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
            var response = await userManager.CreateAsync(user, userCredentials.Password);

            if (response.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("Email", userCredentials.Email),
            };

            var users = await userManager.FindByEmailAsync(userCredentials.Email);
            var clamsDb = await userManager.GetClaimsAsync(users);

            claims.AddRange(clamsDb);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var Creds =  new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: Creds);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration,
            };
        }

        [HttpPost("EsEmpleado")]
        public async Task<ActionResult> EsEmpleado(EmpleadoDTO empleadoDTO)
        {
            var users = await userManager.FindByEmailAsync(empleadoDTO.Email);
            await userManager.AddClaimAsync(users, new Claim("esEmpleado", "1"));
            return NoContent();
        }

         [HttpPost("RemoveEmpleado")]
        public async Task<ActionResult> RemoveEmpleado(EmpleadoDTO empleadoDTO)
        {
            var users = await userManager.FindByEmailAsync(empleadoDTO.Email);
            await userManager.RemoveClaimAsync(users, new Claim("esEmpleado", "1"));
            return NoContent();
        }
    }
}
