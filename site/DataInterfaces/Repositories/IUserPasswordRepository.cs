using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;

namespace DataInterfaces.Repositories
{
    public interface IUserPasswordRepository
    {
        UserPassword GetById(int userId);
    }
}
