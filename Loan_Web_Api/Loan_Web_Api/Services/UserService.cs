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
        User Register(UserRegisterModel user);
        User Login(User user);
        string TokenGenerator(User user);
        User Authenticate(string username, string password);
        User GetUserInfo(User user);


    }
    public class UserService: IUserService
    {
        private UserContext _context;
        private readonly AppSettings _appSettings;
        public UserService(UserContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public string TokenGenerator(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }


        public User Register(UserRegisterModel registerData)
        { 
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

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
            { 
                return null; 
            }

            if (HashSettings.HashPassword(password) != user.Password)
            { 
                return null;
            }

            return user;
        }

        public User Login(User user)
        {
            if (user == null)
            {
                return null;
            }
            string tokenString = TokenGenerator(user);
            var userId = _context.Users.Where(x => x.Username == user.Username).Select(x => x.Id).SingleOrDefault();
            var currentrecord = _context.Users.Find(userId);
            user.Token = tokenString;
            currentrecord.Token = tokenString;
            _context.Users.Update(currentrecord);
            _context.SaveChanges();
            return user;
        }

        public User GetUserInfo(User user)
        {
            throw new NotImplementedException();
        }
    }
}
