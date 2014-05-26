using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;
using DataInterfaces.Repositories;
using System.Data.Entity;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetByUserName(string userName)
        {
            using (var db = new statusContainer())
            {
                var user = db.users.SingleOrDefault(u => u.user_name == userName);
                if (user != null)
                    return new User { UserId = user.user_id, UserName = user.user_name };

                return null;
            }
        }
    }
}
