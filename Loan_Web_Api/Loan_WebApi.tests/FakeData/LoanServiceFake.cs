using Api.Domain;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_WebApi.tests.FakeData
{
    public class LoanServiceFake : ILoanService
    {
        public Loan AddLoan(AddLoanModel loan, int userId)
        {
            var newLoan = new Loan();
            newLoan.UserId = userId;
            newLoan.LoanType = loan.LoanType;
            newLoan.LoanCurrency = loan.Currency;
            newLoan.LoanAmount = loan.Amount;
            newLoan.LoanTime = loan.LoanTime;
            return newLoan;
        }

        public Loan DeleteLoan(int loanId)
        {
            var loans = new List<Loan>() { new Loan() { Id = 2 }, new Loan() { Id = 3 } };
            var loanToDelete = loans.Where(Loan => Loan.Id == loanId).FirstOrDefault();
            loans.Remove(loanToDelete);
            return loanToDelete;
        }

        public IQueryable<Loan> GetOwnLoans(int userId)
        {
            var loans = new List<Loan>() { new Loan() { Id = 1, UserId = 1 }, new Loan() { Id = 2, UserId = 2 } };
            var loansToGet = loans.Where(loan => loan.UserId == userId).AsQueryable();
            return loansToGet;
        }

        public Loan ModifyLoan(ModifyLoanModel model)
        {
            var fakeLoan = new Loan() { Id = model.LoanId };
            if (model.LoanType != null) fakeLoan.LoanType = model.LoanType;
            if (model.Currency != null) fakeLoan.LoanCurrency = model.Currency;
            if (model.Amount != 0) fakeLoan.LoanAmount = model.Amount;
            if (model.LoanTime != 0) fakeLoan.LoanTime = model.LoanTime;
            return fakeLoan;
        }
    }
}
