using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEnity> GetRepository<TEnity>() where TEnity : BaseEntity;
        ISessionRepository SessionRepository { get; set; }
        int SaveChanges();
    }
}
