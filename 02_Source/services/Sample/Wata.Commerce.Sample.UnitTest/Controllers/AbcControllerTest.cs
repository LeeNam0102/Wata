using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Dtos;
using Wata.Commerce.Sample.Business.Services;
using Wata.Commerce.Sample.Controllers;

namespace Wata.Commerce.Sample.UnitTest.Controllers
{
    [TestClass]
    public class AbcControllerTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<AbcController>> _loggerMock;
        private readonly Mock<IAbcService> _abcServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AbcController _abcController;

        public AbcControllerTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<AbcController>>();
            _fixture = new Fixture();
            _abcServiceMock = new Mock<IAbcService>();
            _abcController = new AbcController(_loggerMock.Object, _configurationMock.Object, _abcServiceMock.Object);
        }

        [TestMethod]
        public async Task Insert_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<AbcDto>();
            var requestDto = _fixture.Create<AbcRequestDto>();

            _abcServiceMock.Setup(c => c.InsertAbcAsync(It.IsAny<AbcRequestDto>())).ReturnsAsync(expectedResult);

            // Action
            ActionResult<AbcDto> result = await _abcController.Insert(requestDto);
            var objectResult = (ObjectResult)result.Result;
            AbcDto dtoResult = (AbcDto)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_Success()
        {
            // Arrange
            var requestDto = _fixture.Create<AbcRequestDto>();

            _abcServiceMock.Setup(c => c.UpdateAbcAsync(It.IsAny<AbcRequestDto>())).ReturnsAsync(1);

            // Action
            var result = await _abcController.Update(requestDto, It.IsAny<int>());
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
            var dto = _fixture.Create<AbcDto>();

            // Arrange
            _abcServiceMock.Setup(c => c.GetAbcAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(dto);
            _abcServiceMock.Setup(c => c.DeleteAbcAsync(It.IsAny<int>())).ReturnsAsync(1);

            // Action
            var result = await _abcController.Delete(It.IsAny<int>());
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
            var dto = _fixture.Create<AbcDto>();

            // Arrange
            _abcServiceMock.Setup(c => c.GetAbcAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(dto);

            // Action
            var result = await _abcController.Get(It.IsAny<int>(), It.IsAny<bool>());
            var objectResult = (ObjectResult)result.Result;
            AbcDto dtoResult = (AbcDto)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(dto, dtoResult);
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            var dtos = _fixture.CreateMany<AbcDto>().ToList();
            var expectedResult = new PagedDto<AbcDto>(5, dtos);

            // Arrange
            _abcServiceMock.Setup(c => c.GetListAbcsAsync(It.IsAny<AbcFilterDto>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _abcController.GetList(It.IsAny<AbcFilterDto>());
            var objectResult = (ObjectResult)result.Result;
            PagedDto<AbcDto> pagedDto = (PagedDto<AbcDto>)objectResult.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(expectedResult.Data.Count(), pagedDto.Data.Count());
        }
    }
}