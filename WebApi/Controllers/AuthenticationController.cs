
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<Person> userManager, SignInManager<Person> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("api/signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationCredentials authenticationCredentials)
        {
            var user = await _userManager.FindByNameAsync(authenticationCredentials.Identifier);
            if (user != null)
            {
                if ((await _signInManager.PasswordSignInAsync(user, authenticationCredentials.Password, false, false)).Succeeded)
                {
                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
                        Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["BearerTokens:ExpiryMinutes"]!)),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"]!)), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);

                    return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Je bent succesvol ingelogd.", Token = handler.WriteToken(securityToken) });
                }
            }
            return BadRequest(new { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Je inloggegevens waren incorrect!" });
        }

        [HttpPost("api/signout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Je bent succesvol uitgelogd." });
        }
    }
}