using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using DataInterfaces.Repositories;
using DataInterfaces.Models;
using SiteLogic;

namespace SiteLogic.NUnit
{
    [TestFixture]
    public class AuthenticationTests
    {
        private Mock<IUserRepository> _userRepo;
        private Mock<IUserPasswordRepository> _passwordRepo;

        private const string validLogin = "valid@test.com";
        private const string validPassword = "validPassword";
        private User validUser = new User { UserId = 1, UserName = validLogin };
        private UserPassword validUserPassword = new UserPassword {
            Salt = "$2a$10$w14lcO8AfT9ng5D9FfPz3.", 
            Password = "$2a$10$w14lcO8AfT9ng5D9FfPz3.UOwaFx2CWmUSYG5HyG/3TPHyy5Itv9m" };

        [SetUp]
        public void Setup()
        {
            _userRepo = new Mock<IUserRepository>();
            _userRepo.Setup(r => r.GetByUserName(validLogin)).Returns(validUser);
            _passwordRepo = new Mock<IUserPasswordRepository>();
            _passwordRepo.Setup(r => r.GetById(validUser.UserId)).Returns(validUserPassword);
        }

        [Test]
        public void TestInvalidLogin()
        {
            var Auth = new Authentication(_userRepo.Object, _passwordRepo.Object);

            Assert.AreEqual(null, Auth.AuthenticateUser("test", "test"));
            _userRepo.Verify(r => r.GetByUserName("test"), Times.Once);
            _passwordRepo.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void TestInvalidPassword()
        {
            var Auth = new Authentication(_userRepo.Object, _passwordRepo.Object);

            Assert.AreEqual(null, Auth.AuthenticateUser(validLogin, "test"));
            _userRepo.Verify(r => r.GetByUserName(validLogin), Times.Once);
            _passwordRepo.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void TestValidPassword()
        {
            var Auth = new Authentication(_userRepo.Object, _passwordRepo.Object);

            Assert.AreEqual(validUser.UserId, Auth.AuthenticateUser(validLogin, validPassword));
            _userRepo.Verify(r => r.GetByUserName(validLogin), Times.Once);
            _passwordRepo.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
