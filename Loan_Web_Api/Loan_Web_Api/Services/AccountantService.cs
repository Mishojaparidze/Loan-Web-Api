using Api.Data;
using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Loan_Web_Api.Services
{
    public interface IAccountantService
    {
        public Loan DeleteLoanById(int loanId);
        public Loan GetLoanByID(int loanId);
        public Loan ModifyLoan(int loanId);
        public User BlockUserForLoan(int userId);
        public User UnblockUser(int userId);
    }
    public class AccountantService:IAccountantService
    {
        private UserContext _context;
        private readonly AppSettings _appSettings;
        public AccountantService(UserContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public Loan GetLoanByID(int loanId) 
        {
            return  _context.Loans.Find(loanId);
        }


        public Loan DeleteLoanById(int loanId)
        {
            var loan = _context.Loans.Find(loanId);
            _context.Loans.Remove(loan);
            _context.SaveChangesAsync();
            return loan;
        }

        public Loan ModifyLoan(int loanId)
        {
            var loan = _context.Loans.Find(loanId);
            _context.Loans.Update(loan);
            _context.SaveChangesAsync();
            return loan;
        }

        public User BlockUserForLoan(int userId)
        {
            var user = _context.Users.Find(userId);
            user.IsBlocked = true;
            _context.Users.Add(user);
            _context.SaveChangesAsync();
            return user;
        }

        public User UnblockUser(int userId)
        {
            var user = _context.Users.Find(userId);
            user.IsBlocked = false;
            _context.Users.Add(user);
            _context.SaveChangesAsync();
            return user;
        }
    }
}
