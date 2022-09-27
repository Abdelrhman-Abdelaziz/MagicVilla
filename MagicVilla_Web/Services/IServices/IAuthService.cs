using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTOs;

namespace MagicVilla_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO registerRequestDTO);
    }
}
