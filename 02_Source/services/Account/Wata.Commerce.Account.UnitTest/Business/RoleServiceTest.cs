using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoFixture;
using AutoMapper;
using Moq;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Domain.Models;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Repositories;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Account.Business.Services;
using Wata.Commerce.Account.Business.MapperProfiles;

namespace Wata.Commerce.Account.UnitTest.Business
{
    [TestClass]
    public class RoleServiceTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<RoleService>> _loggerMock;
        private readonly Mock<IRoleRepository> _roleRepositoryMock;
        private readonly IRoleService _roleService;

        public RoleServiceTest()
        {
            _fixture = new Fixture();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<AutoMapperProfile>();
            });

            _mapper = mappingConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<RoleService>>();

            _roleRepositoryMock = new Mock<IRoleRepository>();
            _roleService = new RoleService(_loggerMock.Object, _mapper, _roleRepositoryMock.Object);
        }

        [TestMethod]
        public async Task InsertRoleAsync_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<Role>();
            var requestDto = _fixture.Create<RoleRequestDto>();

            _roleRepositoryMock.Setup(c => c.InsertAsync(It.IsAny<Role>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _roleService.InsertRoleAsync(requestDto);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateRoleAsync_Success()
        {
            // Arrange
            var expectedResult = 1;
            var role = _fixture.Create<Role>();
            var requestDto = _fixture.Create<RoleRequestDto>();

            _roleRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(role);
            _roleRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<Role>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _roleService.UpdateRoleAsync(requestDto);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task DeleteRoleAsync_Success()
        {
            // Arrange
            var expectedResult = 1;
            var requestDto = _fixture.Create<RoleRequestDto>();

            _roleRepositoryMock.Setup(c => c.DeleteAsync(It.IsAny<string>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _roleService.DeleteRoleAsync(It.IsAny<string>());

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task GetRoleAsync_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<Role>();

            _roleRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _roleService.GetRoleAsync(It.IsAny<string>(), It.IsAny<bool>());

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetListRolesAsync_Success()
        {
            // Arrange
            var list = _fixture.CreateMany<Role>().ToList();
            var expectedResult = new PagedDto<Role>(list.Count(), list);

            _roleRepositoryMock.Setup(c => c.GetListAsync(It.IsAny<RoleFilter>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _roleService.GetListRolesAsync(It.IsAny<RoleFilterDto>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.TotalRecords, result.TotalRecords);
        }
    }
}