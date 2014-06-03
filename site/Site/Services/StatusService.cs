using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Site.Models;
using DataInterfaces.Repositories;

namespace Site.Services
{
    public class StatusService : IStatusService
    {
        private IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public StatusList GetHistory(int userId, int pageNumber, int itemsPerPage)
        {
            var items = _statusRepository.GetHistory(userId, pageNumber, itemsPerPage)
                .Select(s => MapStatus(s));

            return new StatusList
            {
                Status = items,
                Page = pageNumber,
                ItemsPerPage = itemsPerPage
            };
        }

        public StatusList Search(int? userId, string searchText, int pageNumber, int itemsPerPage)
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

        public int Add(string Message, int userId)
        {
            return _statusRepository.Add(Message, userId);
        }

        public Status Get(int statusId)
        {
            return MapStatus(_statusRepository.Get(statusId));
        }

        internal Status MapStatus(DataInterfaces.Models.Status status)
        {
            return new Status
                {
                    DateAdded = status.DateAdded,
                    ViewCount = status.Views,
                    LikeCount = status.Likes,
                    Message = status.Message,
                    UserId = status.AddedBy.UserId,
                    UserName = status.AddedBy.UserName,
                };
        }

        internal int CalculatePopularity(int likes, int views)
        {
            var popularity = (likes * 2 + views);
            if (popularity > 100)
                popularity = 100;
            return popularity;
        }
    }
}