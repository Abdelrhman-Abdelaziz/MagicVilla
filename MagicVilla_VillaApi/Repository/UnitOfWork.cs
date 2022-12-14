using MagicVilla_VillaApi.DataAccess;
using MagicVilla_VillaApi.Repository.IRepository;

namespace MagicVilla_VillaApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IVillaRepository Villas { get; init; }
        public IVillaNumberRepository VillaNumbers { get; init; }
        public IUserRepository Users { get; init; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Villas = new VillaRepository(_context);
            VillaNumbers = new VillaNumberRepository(_context);

        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
