using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity? GetByID(int id);
        IEnumerable<TEntity> GetAll(Func<TEntity,bool>? condition = null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
