using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Loan_WebApi.tests.FakeData
{
    public class AccountantServiceFake:IAccountantService
    {
        public Loan DeleteLoanById(int loanId)
        {
            var fakeUser = new User()
            {
                Id = 1,
                Name = "Carmine",
                Surname = "Falcone",
                Age = 48,
                MonthlySalary = 8500,
                Username = "Carmine456",
                Password = HashSettings.HashPassword("Falcone789!"),
                Role = "Accountant",
                IsBlocked = false,
                Loans = new List<Loan> { new Loan { Id = 2},
            new Loan { Id = 3}}
            };
            var deleteLoan = fakeUser.Loans.Where(x => x.Id == loanId).FirstOrDefault();
            fakeUser.Loans.Remove(deleteLoan);
            return deleteLoan;
        }

        public async Task<IQueryable<Loan>> GetLoanByID(int loanId)
        {
            var loan = new List<Loan>
            {
                new Loan{ UserId=1},
                new Loan{ UserId=2},
                new Loan{ UserId=3}
            };
            var result = loan.Where(x => x.UserId == loanId).ToList<Loan>();
            return result.AsQueryable();
        }

        public Loan ModifyLoan(int loanId)
        {
            throw new NotImplementedException();
        }

        public User BlockUserForLoan(int userId)
        {
            throw new NotImplementedException();
        }

        public User UnblockUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> OpenAccountantAccount()
        {
            var fakeAccount = new User()
            {
                Id=1,
                Name = "Carmine",
                Surname = "Falcone",
                Age = 48,
                MonthlySalary = 8500,
                Username = "Carmine456",
                Password = HashSettings.HashPassword("Falcone789!"),
                Role = "Accountant",
                IsBlocked = false
            };
            return fakeAccount;
        }

       

    }
}
