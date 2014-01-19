using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Repositories;
using BCrypt.Net;

namespace SiteLogic
{
    public class Authentication
    {
        private IUserRepository _userRepo;
        private IUserPasswordRepository _passwordRepo;

        public Authentication(IUserRepository userRepo, IUserPasswordRepository passwordRepo)
        {
            _userRepo = userRepo;
            _passwordRepo = passwordRepo;
        }

        public bool AuthenticateUser(string login, string password)
        {
            var user = _userRepo.GetByUserName(login);
            if (user == null)
                return false;

            var passwordDetails = _passwordRepo.GetById(user.UserId);
            var hashedPassword = CalculatePassword(password, passwordDetails.Salt);

            return hashedPassword == passwordDetails.Password;
        }

        public string CalculatePassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}
