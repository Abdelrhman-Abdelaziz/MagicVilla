using MagicVilla_VillaApi.DataAccess;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly AppDbContext _conext;

        public VillaNumberRepository(AppDbContext conext) : base(conext)
        {
            _conext = conext;
        }

        public async Task<IEnumerable<VillaNumber>> GetAllWithVillasAsync()
        {
            return await _conext.VillaNumbers.Include(v => v.Villa).ToListAsync();
        }

    }
}
