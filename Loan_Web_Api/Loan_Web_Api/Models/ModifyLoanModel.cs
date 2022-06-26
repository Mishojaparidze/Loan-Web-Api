using System;

namespace Loan_Web_Api.Models
{
    public class ModifyLoanModel
    {
        public int LoanId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string LoanType { get; set; }
        public TimeSpan LoanTime { get; set; }


    }
}
