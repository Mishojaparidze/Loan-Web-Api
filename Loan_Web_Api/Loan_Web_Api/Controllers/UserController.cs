using Api.Data;
using Api.Domain;
using FluentValidation.Results;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Loan_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _context;
        private IUserService _userService;
        public UserController(IUserService userService, UserContext context)
        {
            _context = context;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.Authenticate(username, password);
            if (user == null) return BadRequest("You entered incorect Username or a Password, Please double check and try it again later.");
            _userService.Login(user);
            return Ok("Success!");
        }

        [AllowAnonymous]
        [HttpPost("register")]
         public async Task<ActionResult<User>> AddUser(UserRegisterModel registerData)
         {
            var validation = new UserValidation(_context);
            ValidationResult result=validation.Validate(registerData);
            if (result.IsValid)
            {
                _userService.Register(registerData);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else 
            {
                return BadRequest(result);
            }
        }

    }
}
