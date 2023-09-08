using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Utility;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Client.Services
{
    public class UserClient : IUserClient
    {
		#region Fields
		private readonly IConfiguration _configuration;
		private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
		#endregion

		#region Constructors
		public UserClient(
			IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _apiUrl = _configuration["Urls:AccountUrl"];
        }
		#endregion

		#region Insert
		public async Task<UserDto?> InsertAsync(UserRequestDto requestDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _apiUrl + "/api/Account/user"))
            {
                requestMessage.Content = JsonContent.Create(requestDto);

                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<UserDto>();
                    }
                }
            }

            return null;
        }
		#endregion

		#region Update
		public async Task<int> UpdateAsync(UserRequestDto requestDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, _apiUrl + "/api/Account/user/" + requestDto.Id))
            {
                requestMessage.Content = JsonContent.Create(requestDto);

                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<int>();
                    }
                }
            }

            return 0;
        }
		#endregion

		#region Delete
		public async Task<int> DeleteAsync(string id)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, _apiUrl + "/api/Account/user/" + id))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<int>();
                    }
                }
            }

            return 0;
        }
		#endregion

		#region Get
		public async Task<UserDto?> GetAsync(string id, bool? isDeep = null)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, _apiUrl + "/api/Account/user/" + id))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<UserDto>();
                    }
                }
            }

            return null;
        }
		#endregion

		#region Get List
		public async Task<PagedDto<UserDto>?> GetListAsync(UserFilterDto filterDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, UrlUtil.BuildQueryUrl(_apiUrl + "/api/Account/user", filterDto)))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        PagedDto<UserDto>? pagedDto = JsonConvert.DeserializeObject<PagedDto<UserDto>>(result);
                        return pagedDto;
                    }
                }
            }

            return null;
        }
		#endregion
	}
}