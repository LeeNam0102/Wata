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
    public class UserServiceTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _fixture = new Fixture();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<AutoMapperProfile>();
            });

            _mapper = mappingConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<UserService>>();

            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_loggerMock.Object, _mapper, _userRepositoryMock.Object);
        }

        [TestMethod]
        public async Task InsertUserAsync_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<User>();
            var requestDto = _fixture.Create<UserRequestDto>();

            _userRepositoryMock.Setup(c => c.InsertAsync(It.IsAny<User>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _userService.InsertUserAsync(requestDto);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateUserAsync_Success()
        {
            // Arrange
            var expectedResult = 1;
            var user = _fixture.Create<User>();
            var requestDto = _fixture.Create<UserRequestDto>();

            _userRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(user);
            _userRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<User>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _userService.UpdateUserAsync(requestDto);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task DeleteUserAsync_Success()
        {
            // Arrange
            var expectedResult = 1;
            var requestDto = _fixture.Create<UserRequestDto>();

            _userRepositoryMock.Setup(c => c.DeleteAsync(It.IsAny<string>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _userService.DeleteUserAsync(It.IsAny<string>());

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task GetUserAsync_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<User>();

            _userRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _userService.GetUserAsync(It.IsAny<string>(), It.IsAny<bool>());

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetListUsersAsync_Success()
        {
            // Arrange
            var list = _fixture.CreateMany<User>().ToList();
            var expectedResult = new PagedDto<User>(list.Count(), list);

            _userRepositoryMock.Setup(c => c.GetListAsync(It.IsAny<UserFilter>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _userService.GetListUsersAsync(It.IsAny<UserFilterDto>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.TotalRecords, result.TotalRecords);
        }
    }
}