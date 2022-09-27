using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAync<T>(string token);
        Task<T> GetAync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateDTO villaDTO, string token);
        Task<T> UpdateAsync<T>(VillaUpdateDTO villaDTO, string token);
        Task<T> DeleteAync<T>(int id, string token);
    }
}
