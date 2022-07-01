using Api.Data;
using Api.Domain;
using FluentValidation.Results;
using FluentValidation;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Logging;

namespace Loan_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _context;
        private IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<UserController> _loggs;
        private ITokenGen _token;


        public UserController(IUserService userService, UserContext context, IOptions<AppSettings> appSettings, ILogger<UserController> loggs, ITokenGen token)
        {
            _context = context;
            _userService = userService;
            _appSettings = appSettings.Value;
            _loggs = loggs;
            _token = token;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(UserLoginModel user)
        {
            var userLogin = _userService.Login(user);
            if (userLogin == null)
            {
                _loggs.LogError("Password or Username was Empty");
                return BadRequest("Password or Username was Empty");
            }
            var token= _token.GenerateToken(userLogin);
            return Ok(new{Username = userLogin.Username,userRole=userLogin.Role, token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
         public async Task<ActionResult<User>> AddUser(UserRegisterModel registerData)
         {
            var validation = new UserValidation(_context);
            ValidationResult result=validation.Validate(registerData);
            if (result.IsValid)
            {
                foreach (var item in (ErrorValidation.GettingError(result)))
                {
                    _loggs.LogError(item);
                }
                
                _userService.Register(registerData);
                await _context.SaveChangesAsync();
                return Ok("User Created");
            }
            else 
            {
                return BadRequest(result);
            }
         }

       /* private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)

                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }*/
    }
}
