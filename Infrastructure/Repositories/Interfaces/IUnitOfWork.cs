namespace Infrastructure.Repositories.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    ISessionRepository SessionRepository { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
