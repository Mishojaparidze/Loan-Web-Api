using Loan_Web_Api.Models;
using Loan_WebApi.tests.FakeData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_WebApi.tests
{
    public class LoanServiceTest:LoanServiceFake
    {

        LoanServiceFake _LoanService;
        public LoanServiceTest(LoanServiceFake loanService)
        {
            _LoanService = loanService;
        }
        [Test]
        public void LoanAdd()
        {
            var addloan = new AddLoanModel() { Amount = 1500, Currency = "gel", LoanTime = 350, LoanType = "FastLoan" };
            var userId = 2;

            var result = _LoanService.AddLoan(addloan, userId);

            Assert.NotNull(result);
        }

        [Test]
        public void LoanDelete()
        {
            var loanId = 1;
            var removedLoan = _LoanService.DeleteLoan(loanId);

            Assert.AreEqual(removedLoan.Id, loanId);
        }
        [Test]
        public void GetOwnLoans()
        {
            var loanCount = 1;
            var gotLoan = _LoanService.GetOwnLoans(1);

            Assert.AreNotEqual(gotLoan.Count(), loanCount);
        }

    }
}
