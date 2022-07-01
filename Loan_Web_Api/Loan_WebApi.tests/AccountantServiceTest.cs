
using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_WebApi.tests.FakeData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_WebApi.tests
{
    public class AccountantServiceTest: AccountantServiceFake
    {
        AccountantServiceFake _accountantService;
        public AccountantServiceTest(AccountantServiceFake accountantService)
        {
            _accountantService=accountantService;
        }

        public List<User> Accountants = new List<User>
        {   new User{
            Id = 1,
            Name = "Carmine",
            Surname = "Falcone",
            Age = 48,
            MonthlySalary = 8500,
            Username = "Carmine456",
            Password = HashSettings.HashPassword("Falcone789!"),
            Role = "Accountant",
            IsBlocked = false,
            Loans=new List <Loan>{ new Loan { Id = 2},
            new Loan { Id = 3}} 
        } };



        [Test]
        public void BlockUser()
        {
            var acc = Accountants[0];
            var userResult = _accountantService.BlockUserForLoan(1);
            Assert.AreNotEqual(acc.IsBlocked, userResult.IsBlocked);
        }

        public void UnblockUser()
        {
            var acc = Accountants[0];
            var userResult = _accountantService.BlockUserForLoan(1);
            Assert.AreEqual(acc.IsBlocked, userResult.IsBlocked);
        }

        public void GetLoanByID()
        {
            var loans = new List<Loan> { new Loan { UserId = 1},
            new Loan{ UserId = 1},
            new Loan {UserId = 2}};

            var result = _accountantService.GetLoanByID(1).Result;
            Assert.AreNotEqual(loans.Count(), result.Count());
        }



    }
}
