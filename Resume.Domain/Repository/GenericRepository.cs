using Microsoft.EntityFrameworkCore;
using Resume.Domain.Context;
using Resume.Domain.Entities.Common;

namespace Resume.Domain.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }



        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        //Create
        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = entity.CreateDate;
            await _dbSet.AddAsync(entity);
        }

        //Get Entity By Id
        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id == entityId);
        }


        //Edit
        public void EditEntity(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }


        //Delete Entity
        public void DeleteEntity(TEntity entity)
        {
            entity.IsDelete = true;
            EditEntity(entity);
        }


        //Find Id then Delete
        public async Task DeleteEntityBy(long entityId)
        {
            TEntity entity = await GetEntityById(entityId);
            if (entity != null) DeleteEntity(entity);
        }

        public void DeletePermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeletePhysically(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeletePermanentBy(long entityId)
        {
            var entity = await GetEntityById(entityId);
            if (entity != null)
            {
                DeletePermanent(entity);
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


        //Dispose
        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }

        }
    }
}
