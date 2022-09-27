using MagicVilla_Utillity;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTOs;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class AuthService : BaseServices, IAuthService
    {
        private readonly IHttpClientFactory _httpClient;
        private string baseUrl;

        public AuthService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            baseUrl = configuration.GetSection("ServicesAPI:VillaAPI").Value;
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Data = loginRequestDTO,
                Url = $"{baseUrl}/api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO registerRequestDTO)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Data = registerRequestDTO,
                Url = $"{baseUrl}/api/UsersAuth/register"
            });
        }
    }
}
