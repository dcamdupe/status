using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Repositories;
using BCrypt.Net;

namespace SiteLogic
{
    public class Authentication : IAuthentication
    {
        private IUserRepository _userRepo;
        private IUserPasswordRepository _passwordRepo;

        public Authentication(IUserRepository userRepo, IUserPasswordRepository passwordRepo)
        {
            _userRepo = userRepo;
            _passwordRepo = passwordRepo;
        }

        public int? AuthenticateUser(string login, string password)
        {
            var user = _userRepo.GetByUserName(login);
            if (user == null)
                return null;

            var passwordDetails = _passwordRepo.GetById(user.UserId);
            var hashedPassword = CalculatePassword(password, passwordDetails.Salt);

            if (hashedPassword == passwordDetails.Password)
                return user.UserId;

            return null;
        }

        internal string CalculatePassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}
