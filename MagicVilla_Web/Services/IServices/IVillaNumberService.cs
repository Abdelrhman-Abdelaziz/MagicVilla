using MagicVilla_Web.Models.DTOs;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAync<T>(string token);
        Task<T> GetAync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNumberCreateDTO villaNumberCreateDTO, string token);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDTO villaNumberUpdateDTO, string token);
        Task<T> DeleteAync<T>(int id, string token);
    }
}
