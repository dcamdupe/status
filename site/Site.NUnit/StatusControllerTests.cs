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
using Site.Controllers;
using System.Web.Mvc;
using Site.Models;

namespace Site.NUnit
{
    [TestFixture]
    public class StatusControllerTests
    {
        private Mock<IStatusService> _statusService;
        private Mock<HttpSessionStateBase> _session;
        private Mock<HttpRequestBase> _request;
        private const int UserId = 1;
        private const string IpAddress = "127.0.0.1";

        [SetUp]
        public void Setup()
        {
            _statusService = new Mock<IStatusService>();
            _session = new Mock<HttpSessionStateBase>();
            _session.Setup(s => s["UserId"]).Returns(UserId);
            _request = new Mock<HttpRequestBase>();
            _request.SetupGet(r => r.UserHostAddress).Returns(IpAddress);
        }

        private StatusController GetController()
        {
            return new StatusController(_statusService.Object, _session.Object, _request.Object);
        }

        [Test]
        public void IndexWithoutPage()
        {
            ValidateIndex(null);
        }

        [Test]
        public void IndexWithPage()
        {
            ValidateIndex(2);
        }

        private void ValidateIndex(int? page)
        {
            var expectedPage = page.HasValue ? page.Value : 1;
            var controller = GetController();
            controller.Index(page);
            _statusService.Verify(s => s.GetHistory(UserId, expectedPage, StatusController.ItemsPerPage), Times.Once());
        }

        [Test]
        public void NewWithoutMessage()
        {
            var controller = GetController();
            var view = controller.New("");
            ((ViewResult)view).ViewName.ShouldEqual("New");
            _statusService.Verify(s => s.Add(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void NewWithMessage()
        {
            const string message = "this is a test";
            var controller = GetController();
            var route = controller.New(message);
            route.ShouldBeType<RedirectToRouteResult>();
            _statusService.Verify(s => s.Add(message, UserId), Times.Once);
        }

        [Test]
        public void Single()
        {
            var controller = GetController();
            var view = controller.Single(1);
            ((ViewResult)view).ViewName.ShouldEqual("Single");
            _statusService.Verify(s => s.Get(1, It.IsAny<ViewUser>()), Times.Once());
        }

        [Test]
        public void SearchWithoutPage()
        {
            ValidateSearch(null);
        }

        [Test]
        public void SearchWithPage()
        {
            ValidateSearch(2);
        }

        private void ValidateSearch(int? page)
        {
            var expectedPage = page.HasValue ? page.Value : 1;
            var controller = GetController();
            var view = controller.Search("test", page);
            ((ViewResult)view).ViewName.ShouldEqual("Search");
            _statusService.Verify(s => s.Search("test", expectedPage, StatusController.ItemsPerPage), Times.Once());
        }

        [Test]
        public void AddLike()
        {
            var controller = GetController();
            var route = controller.AddLike(1);
            route.ShouldBeType<RedirectToRouteResult>();
            _statusService.Verify(s => s.AddLike(1, IpAddress, UserId), Times.Once());
        }
    }
}
