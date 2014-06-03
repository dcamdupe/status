using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Site.Models;

namespace Site.Services
{
    public interface IStatusService
    {
        StatusList GetHistory(int userId, int pageNumber, int itemsPerPage);
        SearchResults Search(string searchText, int pageNumber, int itemsPerPage);
        void AddLike(int statusId, string IpAdress, int? userId);
        void AddView(int statusId, string IpAdress, int? userId);
        int Add(string Message, int userId);
        Status Get(int statusId);

    }
}
