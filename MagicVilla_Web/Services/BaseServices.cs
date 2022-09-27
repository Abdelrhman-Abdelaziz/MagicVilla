using MagicVilla_Utillity;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseServices : IBaseServise
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory HttpClient { get; set; }

        public BaseServices(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            HttpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "Application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), 
                        Encoding.UTF8, "Application/json");
                }

                message.Method = apiRequest.ApiType switch
                {
                    ApiType.GET => HttpMethod.Get,
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.UPDATE => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",apiRequest.Token);
                }
                var response = await client.SendAsync(message);
                var content = await response.Content.ReadAsStringAsync();

                var APIResponse = JsonConvert.DeserializeObject<T>(content);

                return APIResponse;
            }
            catch (Exception ex)
            {

                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);

                return APIResponse;

            }
        }
    }
}
