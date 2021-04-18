using System.Threading.Tasks;
using Pzph.ServiceLayer.Common;

namespace Pzph.RepositoryLayer
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly PzphDbContext _dbContext;

        public Repository(PzphDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Get(string id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}