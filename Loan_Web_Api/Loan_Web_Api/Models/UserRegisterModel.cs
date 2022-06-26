namespace Loan_Web_Api.Models
{
    public class UserRegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int MonthlySalary { get; set; }
        public int Age { get; set; }
    }
}
