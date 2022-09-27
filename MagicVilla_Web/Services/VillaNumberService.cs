using MagicVilla_Utillity;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTOs;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseServices, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClient;
        private string baseUrl;

        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            baseUrl = configuration.GetSection("ServicesAPI:VillaAPI").Value;
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDTO villaNumberCreateDTO, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Data = villaNumberCreateDTO,
                Url = $"{baseUrl}/api/VillaNumber",
                Token = token
            });
        }

        public Task<T> DeleteAync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"{baseUrl}/api/VillaNumber/{id}",
                Token = token
            });
        }

        public Task<T> GetAllAync<T>(string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = $"{baseUrl}/api/VillaNumber",
                Token = token
            });
        }

        public Task<T> GetAync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = $"{baseUrl}/api/VillaNumber/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO villaNumberUpdateDTO, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.PUT,
                Data = villaNumberUpdateDTO,
                Url = $"{baseUrl}/api/VillaNumber/{villaNumberUpdateDTO.VillaNo}",
                Token = token
            });
        }
    }
}
