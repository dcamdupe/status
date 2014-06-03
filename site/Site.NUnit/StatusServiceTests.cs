using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using SiteLogic;
using Site;
using Site.Services;
using System.Web;
using DataInterfaces.Models;
using DataInterfaces.Repositories;
using Should;

namespace Site.NUnit
{
    [TestFixture]
    public class StatusServiceTests
    {
        private Mock<IStatusRepository> _statusRepo;
        private const int UserId = 1;
        private const int PageNumber = 1;
        private const int ItemsPerPage = 10;
        private StatusList _statusList;
        private const string SearchString = "test";
        private Site.Models.ViewUser _viewUser;

        [SetUp]
        public void Setup()
        {
            _statusRepo = new Mock<IStatusRepository>();

            _statusList = new StatusList { TotalItems = 10 , Items = new List<Status>() };
            for (var i = 1; i <= 10; i++)
            {
                _statusList.Items.Add(new Status { AddedBy = new User { UserId = UserId, UserName = "A User" }, DateAdded = DateTime.Now.AddDays(-1 * i), Likes = i, Views = i, Message = i.ToString(), StatusId = i });
            }

            _statusRepo.Setup(s => s.GetHistory(UserId, PageNumber, ItemsPerPage)).Returns(_statusList);
            _statusRepo.Setup(s => s.Search(null, SearchString, PageNumber, ItemsPerPage)).Returns(_statusList);
            _statusRepo.Setup(s => s.Get(It.IsAny<int>())).Returns(_statusList.Items[0]);

            _viewUser = new Models.ViewUser { UserId = UserId, IpAddress = "127.0.0.1" };
        }

        private StatusService GetService()
        {
            return new StatusService(_statusRepo.Object);
        }

        [Test]
        public void Get()
        {
            var svc = GetService();
            var result = svc.Get(1, _viewUser);
            _statusRepo.Verify(s => s.Get(1), Times.Once);
            _statusRepo.Verify(s => s.AddView(1, _viewUser.IpAddress, _viewUser.UserId));
        }

        [Test]
        public void Add()
        {
            var svc = GetService();
            var result = svc.Add("aaa", 1);
            _statusRepo.Verify(s => s.Add("aaa", 1), Times.Once);
        }

        [Test]
        public void GetHistory()
        {
            var svc = GetService();
            var result = svc.GetHistory(UserId, PageNumber, ItemsPerPage);

            result.Page.ShouldEqual(PageNumber);
            result.ItemsPerPage.ShouldEqual(ItemsPerPage);
            result.Status.Count().ShouldEqual(_statusList.Items.Count());

            _statusRepo.Verify(s => s.GetHistory(UserId, PageNumber, ItemsPerPage), Times.Once);
        }

        [Test]
        public void Search()
        {
            var svc = GetService();
            var result = svc.Search(SearchString, PageNumber, ItemsPerPage);

            result.Search.ShouldEqual(SearchString);
            result.StatusList.Page.ShouldEqual(PageNumber);
            result.StatusList.ItemsPerPage.ShouldEqual(ItemsPerPage);
            result.StatusList.Status.Count().ShouldEqual(_statusList.Items.Count());

            _statusRepo.Verify(s => s.Search(null, SearchString, PageNumber, ItemsPerPage), Times.Once);
        }

        [Test]
        public void MapStatus()
        {
            var svc = GetService();
            var result = svc.MapStatus(_statusList.Items[0]);
            result.Message.ShouldEqual(_statusList.Items[0].Message);
            result.LikeCount.ShouldEqual(_statusList.Items[0].Likes);
            result.DateAdded.ShouldEqual(_statusList.Items[0].DateAdded);
            result.UserId.ShouldEqual(_statusList.Items[0].AddedBy.UserId);
            result.UserName.ShouldEqual(_statusList.Items[0].AddedBy.UserName);
            result.ViewCount.ShouldEqual(_statusList.Items[0].Views);
            result.StatusId.ShouldEqual(_statusList.Items[0].StatusId);
        }

        [Test]
        public void MapStatusList()
        {
            var svc = GetService();
            var statusList = new List<Site.Models.Status>
            {
                new Site.Models.Status()
            };
            var result = svc.MapStatusList(statusList, 1, 10, 102);

            result.ItemsPerPage.ShouldEqual(10);
            result.Page.ShouldEqual(1);
            result.PageCount.ShouldEqual(11);
            CollectionAssert.AreEqual(statusList, result.Status);
        }
        
        [Test]
        public void CalculatePopularityTest1()
        {
            var svc = GetService();
            svc.CalculatePopularity(1, 2).ShouldEqual(4);
        }

        [Test]
        public void CalculatePopularityTest2()
        {
            var svc = GetService();
            svc.CalculatePopularity(2, 1).ShouldEqual(5);
        }

        [Test]
        public void CalculatePopularityWithOveflow()
        {
            var svc = GetService();
            svc.CalculatePopularity(50, 2).ShouldEqual(100);
        }
    }
}
