namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface  IUnitOfWork
    {
        public IVillaRepository Villas { get; }
        public IVillaNumberRepository VillaNumbers { get; }
        Task SaveAsync();
    }
}
