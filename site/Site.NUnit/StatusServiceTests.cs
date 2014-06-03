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
        private List<Status> _statusList;

        [SetUp]
        public void Setup()
        {
            _statusRepo = new Mock<IStatusRepository>();

            _statusList = new List<Status>();
            for (var i = 1; i <= 10; i++)
            {
                _statusList.Add(new Status { AddedBy = new User { UserId = UserId, UserName = "A User" }, DateAdded = DateTime.Now.AddDays(-1 * i), Likes = i, Views = i, Message = i.ToString(), StatusId = i });
            }

            _statusRepo.Setup(s => s.GetHistory(UserId, PageNumber, ItemsPerPage)).Returns(_statusList);
            _statusRepo.Setup(s => s.Get(It.IsAny<int>())).Returns(_statusList[0]);
        }

        private StatusService GetService()
        {
            return new StatusService(_statusRepo.Object);
        }

        [Test]
        public void Get()
        {
            var svc = GetService();
            var result = svc.Get(1);
            _statusRepo.Verify(s => s.Get(1), Times.Once);
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
            result.Status.Count().ShouldEqual(_statusList.Count());

            _statusRepo.Verify(s => s.GetHistory(UserId, PageNumber, ItemsPerPage), Times.Once);
        }

        [Test]
        public void MapStatus()
        {
            var svc = GetService();
            var result = svc.MapStatus(_statusList[0]);
            result.Message.ShouldEqual(_statusList[0].Message);
            result.LikeCount.ShouldEqual(_statusList[0].Likes);
            result.DateAdded.ShouldEqual(_statusList[0].DateAdded);
            result.UserId.ShouldEqual(_statusList[0].AddedBy.UserId);
            result.UserName.ShouldEqual(_statusList[0].AddedBy.UserName);
            result.ViewCount.ShouldEqual(_statusList[0].Views);
            result.StatusId.ShouldEqual(_statusList[0].StatusId);
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
