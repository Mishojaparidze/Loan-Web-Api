using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LoanAmount { get; set; }
        public string LoanCurrency { get; set; }
        public string LoanStatus { get; set; }  
        public string LoanType { get; set; } 
        public TimeSpan LoanTime { get; set; }
        public User User { get; set; }
        public Loan()
        {
            LoanStatus = Status.Processing;
        }
    }


        public class Type
        {
            public const string BuyWithCredit = "Buy with Credit";
            public const string FastLoan = "Fast Loan";
            public const string CarLoan = "Car Loan";
            
            
        }
        public class Status
        {
            public const string Processing = "processing";
            public const string Approved = "Approved";
            public const string Declined = "Declined";
        }
        public class Currency
        {
            public const string GEL = "GEL";
            public const string USD = "USD";
        }
    
}
