using MagicVilla_VillaApi.DataAccess;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository.IRepository;


namespace MagicVilla_VillaApi.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly AppDbContext _conext;
        public VillaRepository(AppDbContext conext) : base(conext)
        {
            _conext = conext;
        }

    }
}
