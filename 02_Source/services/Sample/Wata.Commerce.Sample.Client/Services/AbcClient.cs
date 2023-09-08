using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Utility;
using Wata.Commerce.Sample.Dtos;

namespace Wata.Commerce.Sample.Client.Services
{
    public class AbcClient : IAbcClient
    {
		#region Fields
		private readonly IConfiguration _configuration;
		private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
		#endregion

		#region Constructors
		public AbcClient(
			IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _apiUrl = _configuration["Urls:SampleUrl"];
        }
		#endregion

		#region Insert
		public async Task<AbcDto?> InsertAsync(AbcRequestDto requestDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _apiUrl + "/api/Sample/abc"))
            {
                requestMessage.Content = JsonContent.Create(requestDto);

                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<AbcDto>();
                    }
                }
            }

            return null;
        }
		#endregion

		#region Update
		public async Task<int> UpdateAsync(AbcRequestDto requestDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, _apiUrl + "/api/Sample/abc/" + requestDto.AbcID))
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
		public async Task<int> DeleteAsync(int abcID)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, _apiUrl + "/api/Sample/abc/" + abcID))
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
		public async Task<AbcDto?> GetAsync(int abcID, bool? isDeep = null)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, _apiUrl + "/api/Sample/abc/" + abcID))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        return await responseMessage.Content.ReadFromJsonAsync<AbcDto>();
                    }
                }
            }

            return null;
        }
		#endregion

		#region Get List
		public async Task<PagedDto<AbcDto>?> GetListAsync(AbcFilterDto filterDto)
        {
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, UrlUtil.BuildQueryUrl(_apiUrl + "/api/Sample/abc", filterDto)))
            {
                using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        PagedDto<AbcDto>? pagedDto = JsonConvert.DeserializeObject<PagedDto<AbcDto>>(result);
                        return pagedDto;
                    }
                }
            }

            return null;
        }
		#endregion
	}
}