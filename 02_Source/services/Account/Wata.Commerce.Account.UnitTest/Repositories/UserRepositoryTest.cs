using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MockQueryable.Moq;
using AutoFixture;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Data.Repositories;
using Wata.Commerce.Account.Data.Context;
using Wata.Commerce.Account.Domain.Models;
using Wata.Commerce.Account.Domain.Filters;

namespace Wata.Commerce.Account.UnitTest.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        protected readonly Mock<AccountContext> _contextMock;
        private readonly Mock<ILogger<UserRepository>> _loggerMock;
        private readonly UserRepository _userRepository;
        private readonly Fixture _fixture;

        public UserRepositoryTest()
        {
            _contextMock = new Mock<AccountContext>();
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<UserRepository>>();
            _userRepository = new UserRepository(_contextMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetUserById_Success()
        {
            // Arrange
            var id = String.Empty;
            var expectedResult = _fixture.CreateMany<User>();
            if (expectedResult.Count() > 0)
            {
                expectedResult.First().Id = id;
            }

            var users = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Users).Returns(users.Object);

            // Action
            User? result = await _userRepository.GetByIdAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            // Arrange
            var expectedResult = _fixture.CreateMany<User>();

            var users = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Users).Returns(users.Object);
            _contextMock.Setup(c => c.Set<User>()).Returns(users.Object);

            // Action
            IEnumerable<User> result = await _userRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            // Arrange
            var expectedResult = _fixture.CreateMany<User>();

            var users = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Users).Returns(users.Object);

            // Action
            PagedDto<User> result = await _userRepository.GetListAsync(new UserFilter());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Count(), result.Data.Count);
        }

        [TestMethod]
        public async Task InsertUser_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<User>();
            var usersMany = _fixture.CreateMany<User>();
            int expectCount = 1;

            var users = usersMany.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Users).Returns(users.Object);
            _contextMock.Setup(c => c.Set<User>()).Returns(users.Object);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectCount);

            // Action
            User? result = await _userRepository.InsertAsync(expectedResult);

            // Assert
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(result);
        }
    }
}