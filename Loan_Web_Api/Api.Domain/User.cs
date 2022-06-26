using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain
{
    public class User
    {

        public User()
        {
            Role = Roles.User;
            IsBlocked = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public int MonthlySalary { get; set; }
        public int Age { get; set; }    
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
        public string Token { get; set; }
        
        public List<Loan> Loans { get; set; } = new List<Loan>();


    }
}
