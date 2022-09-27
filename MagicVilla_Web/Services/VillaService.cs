using MagicVilla_Utillity;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseServices, IVillaService
    {
        private readonly IHttpClientFactory _httpClient;
        private string baseUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            baseUrl = configuration.GetSection("ServicesAPI:VillaAPI").Value;
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO villaDTO,string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Data = villaDTO,
                Url = $"{baseUrl}/api/Villa",
                Token = token
            });
        }

        public Task<T> DeleteAync<T>(int id,string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"{baseUrl}/api/Villa/{id}",
                Token = token
            });
        }

        public Task<T> GetAllAync<T>(string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = $"{baseUrl}/api/Villa",
                Token = token
            });
        }

        public Task<T> GetAync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = $"{baseUrl}/api/Villa/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO villaDTO, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.PUT,
                Data = villaDTO,
                Url = $"{baseUrl}/api/Villa/{villaDTO.Id}",
                Token = token
            });
        }
    }
}
