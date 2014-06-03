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

namespace Site.NUnit
{
    [TestFixture]
    public class StatusControllerTests
    {
        private Mock<IStatusService> _statusService;
        private Mock<HttpSessionStateBase> _session;
        private const int UserId = 1;

        [SetUp]
        public void Setup()
        {
            _statusService = new Mock<IStatusService>();
            _session = new Mock<HttpSessionStateBase>();
            _session.Setup(s => s["UserId"]).Returns(UserId);
        }

        private StatusController GetController()
        {
            return new StatusController(_statusService.Object, _session.Object);
        }

        [Test]
        public void Index()
        {
            var controller = GetController();
            controller.Index(null);
            _statusService.Verify(s => s.GetHistory(UserId, 1, StatusController.ItemsPerPage), Times.Once());
        }
    }
}
