using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private Mock<IAccountService> _mockAccountService;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUser_ReturnAccountForCorrectUsername()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            Account account = await _authenticationService.Login(expectedUsername, password);

            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithCorrectPasswordForExistingUser_ThrowsInvalidPasswordException()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() =>  _authenticationService.Login(expectedUsername, password));

            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUser_ThrowsInvalidPasswordException()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnPasswordDoNotMatch()
        {
            RegistrationResult expected = RegistrationResult.PasswordsDoNotMatch;
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";

            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnEmailAlreadyExists()
        {
            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;
            string email = "test@gmail.com";
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());

            RegistrationResult actual = await _authenticationService.Register(email, It.IsAny<string>(), "test", "test");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnUsernameAlreadyExists()
        {
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;
            string username = "testuser";
            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());

            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), username, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnSuccess()
        {
            RegistrationResult expected = RegistrationResult.Success;

            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }
    }
}
