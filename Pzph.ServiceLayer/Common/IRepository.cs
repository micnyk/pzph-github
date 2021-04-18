using System.Threading.Tasks;

namespace Pzph.ServiceLayer.Common
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Get(string id);
        Task Add(TEntity entity);
        Task Save();
    }
}