using Api.Data;
using Api.Domain;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

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


        [Authorize(Roles = Roles.User)]
        [HttpPost("addloan")]
        public IActionResult AddLoan(AddLoanModel loan)
        {
            var claimsId = this.User.Identity as ClaimsIdentity;
            var userIdString = claimsId.FindFirst(ClaimTypes.Name)?.Value;
            var userId = Convert.ToInt32(userIdString);
            if (_context.Users.Find(userId).IsBlocked == true) return Unauthorized("Sorry,User is blocked for new Loan");
            _loanService.AddLoan(loan, userId);
            return Ok("You created new Loan application!");

        }



    }
}
