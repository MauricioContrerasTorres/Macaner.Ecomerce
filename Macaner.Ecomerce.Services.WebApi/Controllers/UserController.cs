using Microsoft.AspNetCore.Mvc;
using Macaner.Ecomerce.Application.DTO;
using Macaner.Ecomerce.Application.Interface;
using Macaner.Ecomerce.Services.WebApi.Helpers;
using Macaner.Ecomerce.Transversal.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Macaner.Ecomerce.Services.WebApi.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUsersApplication _usersApplication;
        private readonly AppSettings _appSettings;

        public UserController(IUsersApplication usersApplication, IOptions<AppSettings> appSettings)
        {
            _usersApplication = usersApplication;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [Route("api/Users/Authenticate")]
        [HttpPost]
        public IActionResult Authenticate(UsersDTO authDTO)
        {
            var response = _usersApplication.Authenticate(authDTO.Login, authDTO.Password);
            if (response.IsSuccess)
            {
                if(response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest(response.Message);
        }

        private string BuildToken(Response<UsersDTO> userDTO) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userDTO.Data.IdUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
