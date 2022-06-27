using Api.Data;
using Api.Domain;
using FluentValidation;
using Loan_Web_Api.Models;
using System.Collections.Generic;

namespace Loan_Web_Api.Services
{
    public class LoanValidation : AbstractValidator<AddLoanModel>
    {
        UserContext _context;
        public LoanValidation(UserContext context)
        {
            _context = context;


            RuleFor(AddLoanModel => AddLoanModel.LoanType)
                    .NotEmpty().NotNull().WithMessage("Loan Type is required")
                    .Must(LoanType).WithMessage($"Invalid Loan Type. Loan  must be one of the following: {Type.FastLoan},{Type.CarLoan}{Type.BuyWithCredit}");
            RuleFor(AddLoanModel => AddLoanModel.Currency).NotEmpty().NotNull().Must(choosecurrency).WithMessage
                ($"Invalid Loan Currency. Loan Currency must be one of the following: {Currency.GEL},{Currency.USD}");
            RuleFor(AddLoanModel => AddLoanModel.Amount).NotEmpty().NotNull().LessThan(1000000).GreaterThan(100).WithMessage("Amount must be greater than 100 and less than 1 000 000");
        }

        private bool LoanType(string type)
        {
            List<string> loanTypes = new List<string>()
                {
                    Type.FastLoan.ToLower  (),
                    Type.CarLoan.ToLower(),
                    Type.BuyWithCredit.ToLower(),

                };
            var loanTypeLower = type.ToLower();
            if (loanTypes.Contains(loanTypeLower)) return true;
            else return false;

        }
        public AddLoanModel ConvertTovalidation(Loan loan)
        {
            AddLoanModel loanModel = new AddLoanModel();
            loanModel.Currency = loan.LoanCurrency;
            loanModel.Amount = loan.LoanAmount;
            loanModel.LoanType = loan.LoanType;
            loanModel.LoanTime= loan.LoanTime;
            
            return loanModel;
        }
        private bool choosecurrency(string currency)
        {
            List<string> choosecurrency = new List<string>()
                {
                    Currency.GEL.ToLower(),
                    Currency.USD.ToLower(),
                };
            var loanTypeLower = currency.ToLower();
            if (choosecurrency.Contains(loanTypeLower))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
