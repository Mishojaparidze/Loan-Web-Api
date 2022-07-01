using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Models;
using Loan_Web_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_WebApi.tests.FakeData
{
    public class UserServiceFake : IUserService
    {
        public User GetUserInfo(int id)
        {
            var user = new User()
            {
                Id = 1,
                Name = "Poison",
                Surname = "Ivy",
                Age = 28,
                MonthlySalary = 1500,
                Username = "PoisonIvy",
                Password = HashSettings.HashPassword("PoisonIvy123!"),
                Role = "User",
                IsBlocked = false
            };
            
            return user;

        }

        public User Login(UserLoginModel login)
        {
            var user = new User()
            {
                Id = 1,
                Name = "Poison",
                Surname = "Ivy",
                Age = 28,
                MonthlySalary = 1500,
                Username = "PoisonIvy",
                Password = HashSettings.HashPassword("PoisonIvy123!"),
                Role = "User",
                IsBlocked = false
            };
            if (string.IsNullOrEmpty(login.UserName) || (string.IsNullOrEmpty(login.Password)))
            {
                return null;
            }
            if (user == null)
            {
                return null;
            }
            if (HashSettings.HashPassword(login.Password) != user.Password)
            {
                return null;
            }
            return user;
        }

        public User Register(UserRegisterModel registerData)
        {
            User user = new User();
            user.Name = registerData.Name;
            user.Surname = registerData.Surname;
            user.Username = registerData.UserName;
            user.Password = HashSettings.HashPassword(registerData.Password);
            user.Age = registerData.Age;
            user.MonthlySalary = registerData.MonthlySalary;
            return user;
        }
    }
}
