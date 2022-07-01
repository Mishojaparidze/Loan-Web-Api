using Api.Data;
using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Loan_Web_Api.Services
{
    public interface ILoanService
    {
        public Loan AddLoan(AddLoanModel loanModel, int userId);
        public Loan ModifyLoan(ModifyLoanModel model);
        public Loan DeleteLoan(int loanId);
        public IQueryable<Loan> GetOwnLoans(int userId);
    }
    public class LoanService: ILoanService
    {
        private UserContext _context;
        private readonly AppSettings _appSettings;
        public LoanService(UserContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public Loan AddLoan(AddLoanModel loan, int userId)
        {
            try
            {
                var newLoan = new Loan();
                newLoan.UserId = userId;
                newLoan.LoanType = loan.LoanType;
                newLoan.LoanCurrency = loan.Currency;
                newLoan.LoanAmount = loan.Amount;
                newLoan.LoanTime = loan.LoanTime;
                _context.Loans.Add(newLoan);
                _context.SaveChanges();
                return newLoan;
            }
            
            catch 
            {
                return null;
            }
        }

        

        public Loan ModifyLoan(ModifyLoanModel modify)
        {
            try{var tempLoan = new Loan() { Id = modify.LoanId };
            if (modify.LoanType != null)
            {
                tempLoan.LoanType = modify.LoanType;
            }
            else
            {
                tempLoan.LoanType = _context.Loans.Where(loan => loan.Id == modify.LoanId).FirstOrDefault().LoanType;
            }
            if (modify.Currency != null)
            {
                tempLoan.LoanCurrency = modify.Currency;
            }
            else 
            {
                tempLoan.LoanCurrency = _context.Loans.Where(loan => loan.Id == modify.LoanId).FirstOrDefault().LoanCurrency;
            }
            if (modify.Amount != 0)
            {
                tempLoan.LoanAmount = modify.Amount;
            }
            else
            { 
                tempLoan.LoanAmount = _context.Loans.Where(loan => loan.Id == modify.LoanId).FirstOrDefault().LoanAmount;
            }
            if (modify.LoanTime>0)
            {
                tempLoan.LoanTime = modify.LoanTime;
            }
            else
            {
                tempLoan.LoanTime = _context.Loans.Where(loan => loan.Id == modify.LoanId).FirstOrDefault().LoanTime;
            }
            return tempLoan;
            }
            catch
            {
                return null;
            }
        }
        public IQueryable<Loan> GetOwnLoans(int userId)
        {
            return _context.Loans.Where(loan => loan.UserId == userId);
        }

        public Loan DeleteLoan(int loanId)
        {
            try { 
            var deleteLoan = _context.Loans.Find(loanId);
            _context.Loans.Remove(deleteLoan);
            _context.SaveChanges();
            return deleteLoan;
            }
            catch
            {
                return null;
            }
        }

    }
}
