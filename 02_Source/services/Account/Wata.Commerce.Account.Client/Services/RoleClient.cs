using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Utility;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Client.Services
{
    public class RoleClient : IRoleClient
    {
		#region Fields
		private readonly IConfiguration _configuration;
		private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
		#endregion

		#region Constructors
		public RoleClient(
			IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _apiUrl = _configuration["Urls:AccountUrl"];
        }
		#endregion

		#region Insert
		public async Task<RoleDto?> InsertAsync(RoleRequestDto requestDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _apiUrl + "/api/Account/role"))
            {
                requestMessage.Content = JsonContent.Create(requestDto);

                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<RoleDto>();
                    }
                }
            }

            return null;
        }
		#endregion

		#region Update
		public async Task<int> UpdateAsync(RoleRequestDto requestDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, _apiUrl + "/api/Account/role/" + requestDto.Id))
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
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, _apiUrl + "/api/Account/role/" + id))
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
		public async Task<RoleDto?> GetAsync(string id, bool? isDeep = null)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, _apiUrl + "/api/Account/role/" + id))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<RoleDto>();
                    }
                }
            }

            return null;
        }
		#endregion

		#region Get List
		public async Task<PagedDto<RoleDto>?> GetListAsync(RoleFilterDto filterDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, UrlUtil.BuildQueryUrl(_apiUrl + "/api/Account/role", filterDto)))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        PagedDto<RoleDto>? pagedDto = JsonConvert.DeserializeObject<PagedDto<RoleDto>>(result);
                        return pagedDto;
                    }
                }
            }

            return null;
        }
		#endregion
	}
}