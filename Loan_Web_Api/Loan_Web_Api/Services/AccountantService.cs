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
        public Task<IQueryable<Loan>> GetLoanByID(int loanId);
        public Loan ModifyLoan(int loanId);
        public User BlockUserForLoan(int userId);
        public User UnblockUser(int userId);
        public Task<User> OpenAccountantAccount();
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

        public async Task<User> OpenAccountantAccount()
        {
            try
            {
                var accountant = new User()
                {
                    Name = "Carmine",
                    Surname = "Falcone",
                    Age = 48,
                    MonthlySalary = 8500,
                    Username = "Carmine456",
                    Password = HashSettings.HashPassword("Falcone789!"),
                    Role = "Accountant",
                    IsBlocked = false
                };
                await _context.Users.AddAsync(accountant);
                await _context.SaveChangesAsync();
                return accountant;
            }
            catch 
            {
                return null;
            }

        }

        public async Task<IQueryable<Loan>> GetLoanByID(int userId) 
        {
            return _context.Loans.Where(x => x.UserId == userId);
        }


        public Loan DeleteLoanById(int loanId)
        {
            try
            {
                var loan = _context.Loans.Find(loanId);
                _context.Loans.Remove(loan);
                _context.SaveChangesAsync();
                return loan;
            }
            catch 
            {
                return null;
            }
           
        }
        

        public Loan ModifyLoan(int loanId)
        {
            try
            {
                var loan = _context.Loans.Find(loanId);
            _context.Loans.Update(loan);
            _context.SaveChangesAsync();
            return loan;
            }
            catch
            {
                return null;
            }
        }

        public User BlockUserForLoan(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
            user.IsBlocked = true;
            _context.Users.Add(user);
            _context.SaveChangesAsync();
            return user;
            }
            catch
            {
                return null;
            }
        }

        public User UnblockUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
            user.IsBlocked = false;
            _context.Users.Add(user);
            _context.SaveChangesAsync();
            return user;
            }
            catch 
            {
                return null;
            }
        }
       
    }
}
