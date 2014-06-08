using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Should;
using Moq;
using SiteLogic;
using Site.Services;
using DataInterfaces.Repositories;
using DataInterfaces.Models;

namespace Site.NUnit
{
    [TestFixture]
    public class ApiServicesTests
    {
        private Mock<IApiSessionRepository> _sessionRepo;
        private Mock<IAuthentication> _authService;
        private const string _validUserName = "1";
        private const string _validpassword = "2";
        private const string _invalidPasswrod = "3";
        private User ValidUser = new User { UserId = 4, UserName = "5" };
        private Session ValidSession = new Session { Expires = DateTime.Now.AddDays(1), SessionId = "6"};
        private string InvalidSessionId = "7";

        [SetUp]
        public void Setup()
        {
            _authService = new Mock<IAuthentication>();
            _authService.Setup(a => a.AuthenticateUser(_validUserName, _validpassword)).Returns(ValidUser);

            _sessionRepo = new Mock<IApiSessionRepository>();
            _sessionRepo.Setup(s => s.GetSession(It.IsAny<int>())).Returns(ValidSession);
            _sessionRepo.Setup(s => s.IsSessionValid(ValidSession.SessionId)).Returns(ValidSession);
            _sessionRepo.Setup(s => s.IsSessionValid(InvalidSessionId)).Returns((Session)null);
        }

        private ApiServices CreateSvc()
        {
            return new ApiServices(_sessionRepo.Object, _authService.Object);
        }

        [Test]
        public void CreateSessionValidUser()
        {
            var svc = CreateSvc();
            var sessionId = svc.CreateSession(_validUserName, _validpassword);
            sessionId.ShouldEqual(ValidSession.SessionId);
            _authService.Verify(a => a.AuthenticateUser(_validUserName, _validpassword), Times.Once);
        }

        [Test]
        public void CreateSessionInValidUser()
        {
            var svc = CreateSvc();
            var sessionId = svc.CreateSession(_validUserName, _invalidPasswrod);
            sessionId.ShouldBeEmpty();
            _authService.Verify(a => a.AuthenticateUser(_validUserName, _invalidPasswrod), Times.Once);
            _sessionRepo.Verify(a => a.GetSession(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void IsSessionValidWithValidSession()
        {
            var svc = CreateSvc();
            var result = svc.IsSessionValid(ValidSession.SessionId);
            result.ShouldBeTrue();
            _sessionRepo.Verify(s => s.IsSessionValid(ValidSession.SessionId), Times.Once);
            _sessionRepo.Verify(s => s.RefreshExpiry(ValidSession.SessionId), Times.Once);
        }

        [Test]
        public void IsSessionValidWithInvalidSession()
        {
            var svc = CreateSvc();
            var result = svc.IsSessionValid(InvalidSessionId);
            result.ShouldBeFalse();
            _sessionRepo.Verify(s => s.IsSessionValid(InvalidSessionId), Times.Once);
            _sessionRepo.Verify(s => s.RefreshExpiry(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void EndSession()
        {
            var svc = CreateSvc();
            svc.EndSession(ValidSession.SessionId);
            _sessionRepo.Verify(s => s.EndSession(It.IsAny<string>()), Times.Once);
        }
    }
}
