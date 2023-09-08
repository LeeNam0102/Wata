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
using Wata.Commerce.Sample.Data.Repositories;
using Wata.Commerce.Sample.Data.Context;
using Wata.Commerce.Sample.Domain.Models;
using Wata.Commerce.Sample.Domain.Filters;

namespace Wata.Commerce.Sample.UnitTest.Repositories
{
    [TestClass]
    public class AbcRepositoryTest
    {
        protected readonly Mock<SampleContext> _contextMock;
        private readonly Mock<ILogger<AbcRepository>> _loggerMock;
        private readonly AbcRepository _abcRepository;
        private readonly Fixture _fixture;

        public AbcRepositoryTest()
        {
            _contextMock = new Mock<SampleContext>();
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<AbcRepository>>();
            _abcRepository = new AbcRepository(_contextMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetAbcById_Success()
        {
            // Arrange
            var id = 1;
            var expectedResult = _fixture.CreateMany<Abc>();
            if (expectedResult.Count() > 0)
            {
                expectedResult.First().AbcID = id;
            }

            var abcs = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Abcs).Returns(abcs.Object);

            // Action
            Abc? result = await _abcRepository.GetByIdAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.AbcID);
        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            // Arrange
            var expectedResult = _fixture.CreateMany<Abc>();

            var abcs = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Abcs).Returns(abcs.Object);
            _contextMock.Setup(c => c.Set<Abc>()).Returns(abcs.Object);

            // Action
            IEnumerable<Abc> result = await _abcRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            // Arrange
            var expectedResult = _fixture.CreateMany<Abc>();

            var abcs = expectedResult.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Abcs).Returns(abcs.Object);

            // Action
            PagedDto<Abc> result = await _abcRepository.GetListAsync(new AbcFilter());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Count(), result.Data.Count);
        }

        [TestMethod]
        public async Task InsertAbc_Success()
        {
            // Arrange
            var expectedResult = _fixture.Create<Abc>();
            var abcsMany = _fixture.CreateMany<Abc>();
            int expectCount = 1;

            var abcs = abcsMany.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Abcs).Returns(abcs.Object);
            _contextMock.Setup(c => c.Set<Abc>()).Returns(abcs.Object);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectCount);

            // Action
            Abc? result = await _abcRepository.InsertAsync(expectedResult);

            // Assert
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(result);
        }
    }
}