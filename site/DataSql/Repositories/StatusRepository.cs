using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;
using DataInterfaces.Repositories;
using System.Data.Entity;

namespace Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private statusContainer _db;

        public StatusRepository(ConnectionDetails connection)
        {
            _db = ConnectionBuilder.Create(connection.ConnectionString);
        }

        public int Add(string message, int userId)
        {
            var status = new status
            {
                message = message,
                user_id = userId,
                date_added = DateTime.Now
            };

            _db.status.AddObject(status);
            _db.SaveChanges();

            return status.status_id;
        }

        public Status Get(int statusId)
        {
            throw new NotImplementedException();
        }

        public void AddLike(int statusId, string IpAdress, int? userId)
        {
            throw new NotImplementedException();
        }

        public void AddView(int statusId, string IpAdress, int? userId)
        {
            throw new NotImplementedException();
        }
        public List<Status> Search(int? userId, string search)
        {
            throw new NotImplementedException();
        }
        public List<Status> GetHistory(int userId, int page, int itemsPerPage)
        {
            throw new NotImplementedException();
        }
    }
}
