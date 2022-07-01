using Api.Data;
using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Loan_Web_Api.Controllers
{
    [Authorize(Roles = Roles.Accountant)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountantController : ControllerBase
    {
        private readonly UserContext _context;
        private IAccountantService _accountantService;
        private readonly ILogger<AccountantController> _loggs;
        private ILoanService _loanService;
        private IUserService _userService;
        private ITokenGen _token;
        public AccountantController(UserContext context, IAccountantService accountantService, ILogger<AccountantController> loggs, ILoanService loanService, IUserService userService, ITokenGen token)
        {
            _context = context;
            _accountantService = accountantService;
            _loggs = loggs;
            _loanService = loanService;
            _userService = userService;
            _token = token; 
        }
        [AllowAnonymous]
        [HttpPost("generateaccountant")]
        public async Task<IActionResult> OpenAccountantAccount()
        {
            
            
            var accountant = await _accountantService.OpenAccountantAccount();
            var tokenString = _token.GenerateToken(accountant);
            accountant.Token = tokenString;
            _context.Users.Update(accountant);
            _context.SaveChanges();
            return Ok($"Accountant Credentials: Username: Carmine456" +
                $"Password: Falcone789!" +
                $"Token: {accountant.Token}");
        }

        [HttpGet("GetLoanByID")]
        public async Task<ActionResult<Loan>> GetLoanByID(UserIdModel UserId)
        {
            if (_context.Users.Find(UserId.UserId) == null)
            {
                _loggs.LogError("404 not found");
                return NotFound("404 not found");
            }
            var loans = _accountantService.GetLoanByID(UserId.UserId);

            return Ok(loans);
        }

        [HttpGet("ModifyLoan")]
        public async Task<ActionResult<Loan>> ModifyLoan(ModifyLoanModel modifyLoan)
        {
            if (_context.Users.Find(modifyLoan.LoanId) == null)
            {
                _loggs.LogError("404 not found");
                return NotFound("404 not found");
            }

            LoanValidation validate = new LoanValidation(_context);
            var tempLoan = _accountantService.ModifyLoan(modifyLoan.LoanId);
            var verifiableLoan = validate.ConvertTovalidation(tempLoan);
            var result = validate.Validate(verifiableLoan);
            if (!result.IsValid)
            {
                return BadRequest(result);
            }
            _accountantService.ModifyLoan(modifyLoan.LoanId);
            _context.SaveChanges();
            
            return Ok("This Loan is Updated");
        }

        [HttpDelete("DeleteLoanById")]
        public async Task<ActionResult<Loan>> DeleteLoanById(LoanIdModel loanId) 
        {
            if (_context.Users.Find(loanId.LoanId) == null)
            {
                _loggs.LogError("404 not found");
                return NotFound("404 not found");
            }
             _accountantService.DeleteLoanById(loanId.LoanId);
            return Ok("This Loan is Deleted");
        }

        [HttpPut("BlockUserForNewLoan")]
        public async Task<ActionResult<User>> BlockUserForLoan(UserIdModel userId)
        {
            if (_context.Users.Find(userId.UserId) == null)
            {
                _loggs.LogError("404 not found");
                return NotFound("404 not found");
            }
             _accountantService.BlockUserForLoan(userId.UserId);
            return Ok("This User is Unblocked");
        }
        [HttpPut("UnblockUser")]
        public async Task<ActionResult<User>> UnblockUser(UserIdModel userId)
        {
            if (_context.Users.Find(userId.UserId) == null)
            {
                _loggs.LogError("404 not found");
                return NotFound("404 not found");
            }
            _accountantService.UnblockUser(userId.UserId);
            return Ok("This User is Unblocked");
        }

        

    }
}
