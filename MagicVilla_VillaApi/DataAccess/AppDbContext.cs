using Microsoft.EntityFrameworkCore;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.DataAccess.Configration;

namespace MagicVilla_VillaApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VillaConfiguration());
        }
    }
}
