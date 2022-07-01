using Api.Data;
using Api.Domain;
using Loan_Web_Api.Helpers;
using Loan_Web_Api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Loan_Web_Api.Services
{


    public interface IUserService
    {
        User Login(UserLoginModel login);
        User Register(UserRegisterModel user);       
        User GetUserInfo(int id);
    }
    public class UserService: IUserService
    {
        private UserContext _context;
        
        public UserService(UserContext context)
        {
            _context = context;
        }
        public User Login(UserLoginModel login)
        {
            try { 
            if (string.IsNullOrEmpty(login.UserName) || (string.IsNullOrEmpty(login.Password)))
            {
                return null;
            }
            var user = _context.Users.FirstOrDefault(x => x.Username == login.UserName);
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
            catch
            {
                return null;
            }
        }


        public User Register(UserRegisterModel registerData)
        {
            try { 
            User user = new User();
            user.Name= registerData.Name;
            user.Surname= registerData.Surname;
            user.Username= registerData.UserName;
            user.Password=HashSettings.HashPassword(registerData.Password);
            user.Age=registerData.Age;
            user.MonthlySalary=registerData.MonthlySalary;
            _context.Users.Add(user);
            return user;
            }
            catch
            {
                return null;
            }
        }

        public User GetUserInfo(int id)
        {
            try { 
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            return user;
            }
            catch
            {
                return null;
            }
        }
       
    }
}
