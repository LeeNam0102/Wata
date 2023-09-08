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
    public class RoleRepositoryTest
    {
        protected readonly Mock<AccountContext> _contextMock;
        private readonly Mock<ILogger<RoleRepository>> _loggerMock;
        private readonly RoleRepository _roleRepository;
        private readonly Fixture _fixture;

        public RoleRepositoryTest()
        {
            _contextMock = new Mock<AccountContext>();
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<RoleRepository>>();
            _roleRepository = new RoleRepository(_contextMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetRoleById_Success()
        {
            // Arrange
            var id = String.Empty;
            var expectedResult = _fixture.CreateMany<Role>();
            if (expectedResult.Count() > 0)
            {
                expectedResult.First().Id = id;
            }

            var roles = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Roles).Returns(roles.Object);

            // Action
            Role? result = await _roleRepository.GetByIdAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            // Arrange
            var expectedResult = _fixture.CreateMany<Role>();

            var roles = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Roles).Returns(roles.Object);
            _contextMock.Setup(c => c.Set<Role>()).Returns(roles.Object);

            // Action
            IEnumerable<Role> result = await _roleRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            // Arrange
            var expectedResult = _fixture.CreateMany<Role>();

            var roles = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Roles).Returns(roles.Object);

            // Action
            PagedDto<Role> result = await _roleRepository.GetListAsync(new RoleFilter());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Count(), result.Data.Count);
        }

        [TestMethod]
        public async Task InsertRole_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<Role>();
            var rolesMany = _fixture.CreateMany<Role>();
            int expectCount = 1;

            var roles = rolesMany.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Roles).Returns(roles.Object);
            _contextMock.Setup(c => c.Set<Role>()).Returns(roles.Object);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectCount);

            // Action
            Role? result = await _roleRepository.InsertAsync(expectedResult);

            // Assert
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(result);
        }
    }
}