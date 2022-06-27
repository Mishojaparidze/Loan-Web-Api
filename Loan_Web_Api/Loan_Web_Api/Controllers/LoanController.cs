using Api.Data;
using Api.Domain;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using System.Linq;

namespace Loan_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly UserContext _context;
        private ILoanService _loanService;
        public LoanController(UserContext context, ILoanService loanService)
        {
            _context = context;
            _loanService = loanService;
        }

        internal int getUserid()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdString = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var userId = Convert.ToInt32(userIdString);
            return userId;
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost("addloan")]
        public IActionResult AddLoan(AddLoanModel addloan)
        {
            LoanValidation validation = new LoanValidation(_context);
            var result = validation.Validate(addloan);
            if (result.IsValid)
            {
                var userId = getUserid();
                if (_context.Users.Find(userId).IsBlocked == true)
                {
                    return Unauthorized("User is blocked for Loans");
                }
                _loanService.AddLoan(addloan,userId);
                return Ok(result);
            }
            return BadRequest(result);

        }

        [Authorize(Roles = Roles.User)]
        [HttpGet("getloan")]
        public IActionResult GetOwnLoans()
        {
            var userId = getUserid();
            return Ok(_loanService.GetOwnLoans(userId));
        }

        [Authorize(Roles = Roles.User)]
        [HttpDelete("DeleteLoan")]
        public async Task<IActionResult> DeleteLoan(LoanIdModel model)
        {
            var userId = getUserid();
            IQueryable<Loan> ownLoans = _loanService.GetOwnLoans(userId);
            var loanToCheck = ownLoans.Where(loan => loan.Id == model.LoanId).FirstOrDefault();
            if (loanToCheck == null)
            {
                return NotFound("404 Not Found");
            }
            if (loanToCheck.LoanStatus != Status.Processing)
            {
                return Unauthorized("You can't modify,Loan is already processed!");
            }
            _loanService.DeleteLoan(model.LoanId);
            return Ok("Loan Deleted");
        }

            [Authorize(Roles = Roles.User)]
        [HttpPut("ModifyLoan")]
        public IActionResult UpdateOwnLoan(ModifyLoanModel modify)
        {
            LoanValidation validation = new LoanValidation(_context);
            var userId = getUserid();
            var tempLoan = _loanService.ModifyLoan(modify);
            tempLoan.UserId = userId;
            if (tempLoan.UserId != _context.Loans.Find(modify.LoanId).UserId)
            {
                return Unauthorized("You can't modify,this is not your Loan!"); 
            }
            if (tempLoan.LoanStatus != Status.Processing)
            {
                return Unauthorized("You can't modify,Loan is already processed!");
            }
            
            var loanVerified=validation.ConvertTovalidation(tempLoan);
            var results = validation.Validate(loanVerified);
            if (results.IsValid)
            {
                _context.Loans.Update(tempLoan);
                _context.SaveChanges();
                return Ok("Loan Updated");
                
            }
            return BadRequest(results);
        }
    }
}
