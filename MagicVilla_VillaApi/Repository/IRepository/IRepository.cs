using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracked = false);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, bool tracked = false);
        Task<TEntity> FindAsync(int id);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        Task<bool> ContainsAsync(int id);

    }
}
