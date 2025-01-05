using Resume.Domain.Entities.Common;

namespace Resume.Domain.Repository;


    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();
        Task AddEntity(TEntity entity);
        Task<TEntity> GetEntityById(long entityId);
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task DeleteEntityBy(long entityId);
        void DeletePermanent(TEntity entity);
        void DeletePhysically(TEntity entity);
        Task SaveChanges();
    
}