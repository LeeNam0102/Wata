using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Account.Business.Services;
using Wata.Commerce.Account.Controllers;

namespace Wata.Commerce.Account.UnitTest.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _fixture = new Fixture();
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_loggerMock.Object, _configurationMock.Object, _userServiceMock.Object);
        }

        [TestMethod]
        public async Task Insert_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<UserDto>();
            var requestDto = _fixture.Create<UserRequestDto>();

            _userServiceMock.Setup(c => c.InsertUserAsync(It.IsAny<UserRequestDto>())).ReturnsAsync(expectedResult);

            // Action
            ActionResult<UserDto> result = await _userController.Insert(requestDto);
            var objectResult = (ObjectResult)result.Result;
            UserDto dtoResult = (UserDto)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_Success()
        {
            // Arrange
            var requestDto = _fixture.Create<UserRequestDto>();

            _userServiceMock.Setup(c => c.UpdateUserAsync(It.IsAny<UserRequestDto>())).ReturnsAsync(1);

            // Action
            var result = await _userController.Update(requestDto, It.IsAny<string>());
            var objectResult = (ObjectResult)result.Result;
            int dtoResult = (int)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsTrue(dtoResult > 0);
        }

        [TestMethod]
        public async Task Delete_Success()
        {
            var dto = _fixture.Create<UserDto>();

            // Arrange
            _userServiceMock.Setup(c => c.GetUserAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(dto);
            _userServiceMock.Setup(c => c.DeleteUserAsync(It.IsAny<string>())).ReturnsAsync(1);

            // Action
            var result = await _userController.Delete(It.IsAny<string>());
            var objectResult = (ObjectResult)result.Result;
            int dtoResult = (int)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsTrue(dtoResult > 0);
        }

        [TestMethod]
        public async Task Get_Success()
        {
            var dto = _fixture.Create<UserDto>();

            // Arrange
            _userServiceMock.Setup(c => c.GetUserAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(dto);

            // Action
            var result = await _userController.Get(It.IsAny<string>(), It.IsAny<bool>());
            var objectResult = (ObjectResult)result.Result;
            UserDto dtoResult = (UserDto)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(dto, dtoResult);
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            var dtos = _fixture.CreateMany<UserDto>().ToList();
            var expectedResult = new PagedDto<UserDto>(5, dtos);

            // Arrange
            _userServiceMock.Setup(c => c.GetListUsersAsync(It.IsAny<UserFilterDto>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _userController.GetList(It.IsAny<UserFilterDto>());
            var objectResult = (ObjectResult)result.Result;
            PagedDto<UserDto> pagedDto = (PagedDto<UserDto>)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(expectedResult.Data.Count(), pagedDto.Data.Count());
        }
    }
}