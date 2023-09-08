using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wata.Commerce.Common.Test;
using Wata.Commerce.Account.Host;
using Wata.Commerce.Account.Client.Services;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.IntegrationTest.Tests
{

    [TestClass]
    public class UserTest : IntegrationTestBase
    {
        public static UserClient _userClient;

        [ClassInitialize]
        public static void TextFixtureSetup(TestContext context)
        {
            _client = new CustomWebApplicationFactory<Program>().CreateClient();

            _userClient = new UserClient(_configuration, _client);
        }

        [TestMethod]
        public async Task Insert_Success()
        {
            UserRequestDto requestDto = new UserRequestDto();
			requestDto.Id = "Id";
			requestDto.AccessFailedCount = 1;
			requestDto.ConcurrencyStamp = "ConcurrencyStamp";
			requestDto.Email = "Email";
			requestDto.EmailConfirmed = false;
			requestDto.LockoutEnabled = false;
			requestDto.LockoutEnd = false;
			requestDto.NormalizedEmail = "NormalizedEmail";
			requestDto.NormalizedUserName = "NormalizedUserName";
			requestDto.PasswordHash = "PasswordHash";
			requestDto.PhoneNumber = "PhoneNumber";
			requestDto.PhoneNumberConfirmed = false;
			requestDto.SecurityStamp = "SecurityStamp";
			requestDto.TwoFactorEnabled = false;
			requestDto.UserName = "UserName";

            var result = await _userClient.InsertAsync(requestDto);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Update_Success()
        {
            UserRequestDto requestDto = new UserRequestDto();
			requestDto.Id = "Id";
			requestDto.AccessFailedCount = 1;
			requestDto.ConcurrencyStamp = "ConcurrencyStamp";
			requestDto.Email = "Email";
			requestDto.EmailConfirmed = false;
			requestDto.LockoutEnabled = false;
			requestDto.LockoutEnd = false;
			requestDto.NormalizedEmail = "NormalizedEmail";
			requestDto.NormalizedUserName = "NormalizedUserName";
			requestDto.PasswordHash = "PasswordHash";
			requestDto.PhoneNumber = "PhoneNumber";
			requestDto.PhoneNumberConfirmed = false;
			requestDto.SecurityStamp = "SecurityStamp";
			requestDto.TwoFactorEnabled = false;
			requestDto.UserName = "UserName";

            var result = await _userClient.UpdateAsync(requestDto);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task Delete_Success()
        {
            var result = await _userClient.DeleteAsync("2");

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task Get_Success()
        {
            var result = await _userClient.GetAsync("1", false);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetList_Success()
        {
            var result = await _userClient.GetListAsync(new UserFilterDto() { IsOutputTotal = true});

            Assert.IsTrue(result.TotalRecords > 0);
            Assert.AreEqual(result.Data.Count, result.TotalRecords);
        }
    }
}