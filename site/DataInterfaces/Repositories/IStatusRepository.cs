using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;

namespace DataInterfaces.Repositories
{
    public interface IStatusRepository
    {
        int Add(string message, int userId);
        Status Get(int statusId);
        void AddLike(int statusId, string IpAdress, int? userId);
        void AddView(int statusId, string IpAdress, int? userId);
        List<Status> Search(int? userId, string search, int page, int itemsPerPage);
        List<Status> GetHistory(int userId, int page, int itemsPerPage);
    }
}
