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

namespace Site.NUnit
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IAuthentication> _authLogic;
        private Mock<HttpContextBase> _httpContext;

        [SetUp]
        public void Setup()
        {
            _authLogic = new Mock<IAuthentication>();
            _httpContext = new Mock<HttpContextBase>();
            _httpContext.SetupAllProperties();
        }

        [Test]
        public void AutheticateUserInvalidUser()
        {
            _authLogic.Setup(a => a.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns((User) null);
            var auth = new AuthenticationService(_authLogic.Object);
            Assert.AreEqual(null, auth.AutheticateUser(_httpContext.Object, "", ""));
            _authLogic.Verify(a => a.AuthenticateUser("", ""), Times.Once);
        }
    }
}
