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
            var history = _statusRepository.GetHistory(userId, pageNumber, itemsPerPage);
            var items = history.Items.Select(s => MapStatus(s));

            return MapStatusList(items, pageNumber, itemsPerPage, history.TotalItems);
        }

        public SearchResults Search(string searchText, int pageNumber, int itemsPerPage)
        {
            var search = _statusRepository.Search(null, searchText, pageNumber, itemsPerPage);
            var items = search.Items.Select(s => MapStatus(s));

            return new SearchResults
            {
                StatusList = MapStatusList(items, pageNumber, itemsPerPage, search.TotalItems),
                Search = searchText
            };
        }

        public StatusList MapStatusList(IEnumerable<Status> statusList, int pageNumber, int itemsPerPage, int totalItems)
        {
            var pages = totalItems / itemsPerPage;
            if (totalItems % itemsPerPage != 0)
                pages++;

            return new StatusList
            {
                Status = statusList,
                Page = pageNumber,
                ItemsPerPage = itemsPerPage,
                PageCount = pages
            };
        }

        public void AddLike(int statusId, string IpAdress, int? userId)
        {
            throw new NotImplementedException();
        }

        public int Add(string Message, int userId)
        {
            return _statusRepository.Add(Message, userId);
        }

        public Status Get(int statusId, ViewUser user)
        {
            var status = _statusRepository.Get(statusId);
            _statusRepository.AddView(statusId, user.IpAddress, user.UserId);
            return MapStatus(status);
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
                    StatusId = status.StatusId
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