using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;
using DataInterfaces.Repositories;
using System.Data.Entity;
using Data.Services;

namespace Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        public int Add(string message, int userId)
        {
            using (var db = new statusContainer())
            {
                var status = new status
                {
                    message = message,
                    user_id = userId,
                    date_added = DateTime.Now
                };

                db.status.AddObject(status);
                db.SaveChanges();

                return status.status_id;
            }
        }

        public Status Get(int statusId)
        {
            using (var db = new statusContainer())
            {
                var status = db.status.Single(s => s.status_id == statusId);
                return MapStatus(status);
            }
        }

        private Status MapStatus(status dbStatus)
        {
            return new Status
            {
                StatusId = dbStatus.status_id,
                Message = dbStatus.message,
                DateAdded = dbStatus.date_added,
                AddedBy = new User { UserId = dbStatus.user.user_id, UserName = dbStatus.user.user_name },
                Likes = dbStatus.status_like.Count(),
                Views = dbStatus.status_view.Count()
            };
        }

        public void AddLike(int statusId, string IpAdress, int? userId)
        {
            using (var db = new statusContainer())
            {
                var like = new status_like
                {
                    status_id = statusId,
                    date_added = DateTime.Now,
                    user_id = userId,
                    ip_address = IpAdress
                };

                db.status_like.AddObject(like);
                db.SaveChanges();
            }
        }

        public void AddView(int statusId, string IpAdress, int? userId)
        {
            using (var db = new statusContainer())
            {
                var view = new status_view
                {
                    status_id = statusId,
                    date_added = DateTime.Now,
                    user_id = userId,
                    ip_address = IpAdress
                };

                db.status_view.AddObject(view);
                db.SaveChanges();
            }
        }
        public List<Status> Search(int? userId, string search)
        {
            using (var db = new statusContainer())
            {
                var statusList = db.status.Where(s => s.message.Contains(search));

                if (userId.HasValue)
                    statusList = statusList.Where(s => s.user_id == userId.Value);

                return statusList.Select(s => MapStatus(s)).ToList();
            }
        }
        public List<Status> GetHistory(int userId, int page, int itemsPerPage)
        {
            using (var db = new statusContainer())
            {
                var paginationDetails = PaginationTools.ConvertPagesToCount(page, itemsPerPage);
                return db.status
                    .Where(s => s.user_id == userId)
                    .Skip(paginationDetails.Item1)
                    .Take(paginationDetails.Item2)
                    .Select(s => MapStatus(s))
                    .ToList();
            }
        }
    }
}
