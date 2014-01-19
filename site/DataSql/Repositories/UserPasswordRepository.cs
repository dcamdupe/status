using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;
using DataInterfaces.Repositories;
using System.Data.Entity;

namespace Data.Repositories
{
    public class UserPasswordRepository : IUserPasswordRepository
    {
        private statusContainer _db;

        public UserPasswordRepository(ConnectionDetails connection)
        {
            _db = ConnectionBuilder.Create(connection.ConnectionString);
        }

        public UserPassword GetById(int userId)
        {
            var pwd = _db.passwords.SingleOrDefault(p => p.user_id == userId);

            if (pwd != null)
                return new UserPassword { Password = pwd.password1, Salt = pwd.salt };

            return null;
        }
    }
}
