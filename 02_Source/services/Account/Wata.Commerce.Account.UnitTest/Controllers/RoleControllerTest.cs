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
    public class RoleControllerTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<RoleController>> _loggerMock;
        private readonly Mock<IRoleService> _roleServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly RoleController _roleController;

        public RoleControllerTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<RoleController>>();
            _fixture = new Fixture();
            _roleServiceMock = new Mock<IRoleService>();
            _roleController = new RoleController(_loggerMock.Object, _configurationMock.Object, _roleServiceMock.Object);
        }

        [TestMethod]
        public async Task Insert_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<RoleDto>();
            var requestDto = _fixture.Create<RoleRequestDto>();

            _roleServiceMock.Setup(c => c.InsertRoleAsync(It.IsAny<RoleRequestDto>())).ReturnsAsync(expectedResult);

            // Action
            ActionResult<RoleDto> result = await _roleController.Insert(requestDto);
            var objectResult = (ObjectResult)result.Result;
            RoleDto dtoResult = (RoleDto)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_Success()
        {
            // Arrange
            var requestDto = _fixture.Create<RoleRequestDto>();

            _roleServiceMock.Setup(c => c.UpdateRoleAsync(It.IsAny<RoleRequestDto>())).ReturnsAsync(1);

            // Action
            var result = await _roleController.Update(requestDto, It.IsAny<string>());
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
            var dto = _fixture.Create<RoleDto>();

            // Arrange
            _roleServiceMock.Setup(c => c.GetRoleAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(dto);
            _roleServiceMock.Setup(c => c.DeleteRoleAsync(It.IsAny<string>())).ReturnsAsync(1);

            // Action
            var result = await _roleController.Delete(It.IsAny<string>());
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
            var dto = _fixture.Create<RoleDto>();

            // Arrange
            _roleServiceMock.Setup(c => c.GetRoleAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(dto);

            // Action
            var result = await _roleController.Get(It.IsAny<string>(), It.IsAny<bool>());
            var objectResult = (ObjectResult)result.Result;
            RoleDto dtoResult = (RoleDto)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(dto, dtoResult);
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            var dtos = _fixture.CreateMany<RoleDto>().ToList();
            var expectedResult = new PagedDto<RoleDto>(5, dtos);

            // Arrange
            _roleServiceMock.Setup(c => c.GetListRolesAsync(It.IsAny<RoleFilterDto>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _roleController.GetList(It.IsAny<RoleFilterDto>());
            var objectResult = (ObjectResult)result.Result;
            PagedDto<RoleDto> pagedDto = (PagedDto<RoleDto>)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(expectedResult.Data.Count(), pagedDto.Data.Count());
        }
    }
}