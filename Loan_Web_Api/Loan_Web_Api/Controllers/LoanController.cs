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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;

namespace Loan_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly UserContext _context;
        private ILoanService _loanService;
        private readonly ILogger<LoanController> _loggs;
        public LoanController(UserContext context, ILoanService loanService, ILogger<LoanController> loggs)
        {
            _context = context;
            _loanService = loanService;
            _loggs = loggs;
           
        }

        [Authorize(Roles = "User")]
        [HttpPost("addloan/{token}")]
        public IActionResult AddLoan([FromBody]AddLoanModel addloan, string token)
        {

            LoanValidation validation = new LoanValidation(_context);
            var result = validation.Validate(addloan);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            _loanService.AddLoan(addloan, GetUserIdFromToken(token));
            return Ok(addloan);
        }


        [Authorize(Roles = Roles.User)]
        [HttpGet("getAllLoans/{token}")]
        public IActionResult GetOwnLoans(string token)
        {
            var userId = GetUserIdFromToken(token);
            var loans = _context.Loans.Where(x=>x.UserId== userId);
            return Ok(loans);
        }

        [Authorize(Roles = Roles.User)]
        [HttpDelete("DeleteLoan/{token}")]
        public async Task<IActionResult> DeleteLoan(LoanIdModel model, string token)
        {
            var userId = GetUserIdFromToken(token);
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
        [HttpPut("ModifyLoan/{token}")]
        public IActionResult UpdateOwnLoan(ModifyLoanModel modify, string token)
        {
            LoanValidation validation = new LoanValidation(_context);
            var userId = GetUserIdFromToken(token);
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

            var loanVerified = validation.ConvertTovalidation(tempLoan);
            var results = validation.Validate(loanVerified);
            if (results.IsValid)
            {
                _context.Loans.Update(tempLoan);
                _context.SaveChanges();
                return Ok("Loan Updated");
            }
            return BadRequest(results);
        }

        private int GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "nameid").Value);
            return userId;
        }

        

    }
}
