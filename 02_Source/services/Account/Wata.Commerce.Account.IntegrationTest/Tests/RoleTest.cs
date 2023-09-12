using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wata.Commerce.Common.Test;
using Wata.Commerce.Account.Host;
using Wata.Commerce.Account.Client.Services;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.IntegrationTest.Tests
{

    [TestClass]
    public class RoleTest : IntegrationTestBase
    {
        public static RoleClient _roleClient;

        [ClassInitialize]
        public static void TextFixtureSetup(TestContext context)
        {
            _client = new CustomWebApplicationFactory<Program>().CreateClient();

            _roleClient = new RoleClient(_configuration, _client);
        }

        [TestMethod]
        public async Task Insert_Success()
        {
            RoleRequestDto requestDto = new RoleRequestDto();
            requestDto.Id = "Id";
            requestDto.ConcurrencyStamp = "ConcurrencyStamp";
            requestDto.Name = "Name";
            requestDto.NormalizedName = "NormalizedName";

            var result = await _roleClient.InsertAsync(requestDto);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Update_Success()
        {
            RoleRequestDto requestDto = new RoleRequestDto();
            requestDto.Id = "Id";
            requestDto.ConcurrencyStamp = "ConcurrencyStamp";
            requestDto.Name = "Name";
            requestDto.NormalizedName = "NormalizedName";

            var result = await _roleClient.UpdateAsync(requestDto);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task Delete_Success()
        {
            var result = await _roleClient.DeleteAsync("2");

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task Get_Success()
        {
            var result = await _roleClient.GetAsync("1", false);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            var result = await _roleClient.GetListAsync(new RoleFilterDto() { IsOutputTotal = true });
            Assert.IsTrue(result.TotalRecords > 0);
            Assert.AreEqual(result.Data.Count, result.TotalRecords);
        }
    }
}