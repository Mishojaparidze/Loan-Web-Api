using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain
{
    public class Accountant
    {
        public Accountant()
        {
            Role = Roles.Accountant;
        }

        public int Id { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User User { get; set; }




    }
}
