using Api.Data;
using Api.Domain;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public AccountantController(UserContext context, IAccountantService accountantService)
        {
            _context = context;
            _accountantService = accountantService; 
        }

        [HttpGet("GetLoanByID")]
        public async Task<ActionResult<Loan>> GetLoanByID(LoanIdModel loanId)
        {
            if (_context.Users.Find(loanId.LoanId) == null)
            {
                return NotFound("404 not found");
            }
            _accountantService.GetLoanByID(loanId.LoanId);
            return Ok("This is the Loan you were looking for:");
        }

        [HttpGet("ModifyLoan")]
        public async Task<ActionResult<Loan>> ModifyLoan(LoanIdModel loanId)
        {
            if (_context.Users.Find(loanId.LoanId) == null)
            {
                return NotFound("404 not found");
            }
            _accountantService.ModifyLoan(loanId.LoanId);
            return Ok("This Loan is Updated");
        }

        [HttpDelete("DeleteLoanById")]
        public async Task<ActionResult<Loan>> DeleteLoanById(LoanIdModel loanId) 
        {
            if (_context.Users.Find(loanId.LoanId) == null)
            {
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
                return NotFound("404 not found");
            }
            _accountantService.UnblockUser(userId.UserId);
            return Ok("This User is Unblocked");
        }

        

    }
}
