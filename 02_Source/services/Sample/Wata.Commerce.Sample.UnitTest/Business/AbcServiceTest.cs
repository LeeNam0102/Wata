using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoFixture;
using AutoMapper;
using Moq;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Domain.Models;
using Wata.Commerce.Sample.Domain.Filters;
using Wata.Commerce.Sample.Domain.Repositories;
using Wata.Commerce.Sample.Dtos;
using Wata.Commerce.Sample.Business.Services;
using Wata.Commerce.Sample.Business.MapperProfiles;

namespace Wata.Commerce.Sample.UnitTest.Business
{
    [TestClass]
    public class AbcServiceTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<AbcService>> _loggerMock;
        private readonly Mock<IAbcRepository> _abcRepositoryMock;
        private readonly IAbcService _abcService;

        public AbcServiceTest()
        {
            _fixture = new Fixture();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<AutoMapperProfile>();
            });

            _mapper = mappingConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<AbcService>>();

            _abcRepositoryMock = new Mock<IAbcRepository>();
            _abcService = new AbcService(_loggerMock.Object, _mapper, _abcRepositoryMock.Object);
        }

        [TestMethod]
        public async Task InsertAbcAsync_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<Abc>();
            var requestDto = _fixture.Create<AbcRequestDto>();

            _abcRepositoryMock.Setup(c => c.InsertAsync(It.IsAny<Abc>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _abcService.InsertAbcAsync(requestDto);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateAbcAsync_Success()
        {
            // Arrange
            var expectedResult = 1;
            var abc = _fixture.Create<Abc>();
            var requestDto = _fixture.Create<AbcRequestDto>();

            _abcRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(abc);
            _abcRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<Abc>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _abcService.UpdateAbcAsync(requestDto);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task DeleteAbcAsync_Success()
        {
            // Arrange
            var expectedResult = 1;
            var requestDto = _fixture.Create<AbcRequestDto>();

            _abcRepositoryMock.Setup(c => c.DeleteAsync(It.IsAny<int>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _abcService.DeleteAbcAsync(It.IsAny<int>());

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task GetAbcAsync_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<Abc>();

            _abcRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _abcService.GetAbcAsync(It.IsAny<int>(), It.IsAny<bool>());

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetListAbcsAsync_Success()
        {
            // Arrange
            var list = _fixture.CreateMany<Abc>().ToList();
            var expectedResult = new PagedDto<Abc>(list.Count(), list);

            _abcRepositoryMock.Setup(c => c.GetListAsync(It.IsAny<AbcFilter>())).ReturnsAsync(expectedResult);

            // Action
            var result = await _abcService.GetListAbcsAsync(It.IsAny<AbcFilterDto>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.TotalRecords, result.TotalRecords);
        }
    }
}