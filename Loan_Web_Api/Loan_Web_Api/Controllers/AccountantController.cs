using Api.Data;
using Api.Domain;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public AccountantController(UserContext context, IAccountantService accountantService, ILogger<AccountantController> loggs)
        {
            _context = context;
            _accountantService = accountantService;
            _loggs = loggs;
        }

        [HttpGet("GetLoanByID")]
        public async Task<ActionResult<Loan>> GetLoanByID(LoanIdModel loanId)
        {
            if (_context.Users.Find(loanId.LoanId) == null)
            {
                _loggs.LogError("404 not found");
                return NotFound("404 not found");
            }
            _accountantService.GetLoanByID(loanId.LoanId);

            return Ok("This is the Loan you were looking for:");
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
