using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wata.Commerce.Common.Test;
using Wata.Commerce.Sample.Host;
using Wata.Commerce.Sample.Client.Services;
using Wata.Commerce.Sample.Dtos;

namespace Wata.Commerce.Sample.IntegrationTest.Tests
{

    [TestClass]
    public class AbcTest : IntegrationTestBase
    {
        public static AbcClient _abcClient;

        [ClassInitialize]
        public static void TextFixtureSetup(TestContext context)
        {
            _client = new CustomWebApplicationFactory<Program>().CreateClient();

            _abcClient = new AbcClient(_configuration, _client);
        }

        [TestMethod]
        public async Task Insert_Success()
        {
            AbcRequestDto requestDto = new AbcRequestDto();
            requestDto.Name = "Name";

            var result = await _abcClient.InsertAsync(requestDto);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Update_Success()
        {
            AbcRequestDto requestDto = new AbcRequestDto();
            requestDto.AbcID = 1;
            requestDto.Name = "Name";

            var result = await _abcClient.UpdateAsync(requestDto);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task Delete_Success()
        {
            var result = await _abcClient.DeleteAsync(2);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task Get_Success()
        {
            var result = await _abcClient.GetAsync(1, false);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            var result = await _abcClient.GetListAsync(new AbcFilterDto() { IsOutputTotal = true });

            Assert.IsTrue(result.TotalRecords > 0);
            Assert.AreEqual(result.Data.Count, result.TotalRecords);
        }
    }
}