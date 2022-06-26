using Api.Data;
using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Models;
using Microsoft.Extensions.Options;

namespace Loan_Web_Api.Services
{
    public interface ILoanService
    {
        public Loan AddLoan(AddLoanModel loanModel, int userId);
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
            var addNewLoan = new Loan();
            addNewLoan.UserId = userId;
            addNewLoan.LoanType = loan.LoanType;
            addNewLoan.LoanCurrency = loan.Currency;
            addNewLoan.LoanAmount = loan.Amount;
            addNewLoan.LoanTime = loan.LoanTime;
            _context.Loans.Add(addNewLoan);
            _context.SaveChanges();
            return addNewLoan;
        }

    }
}
