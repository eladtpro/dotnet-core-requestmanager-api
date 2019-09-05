using RequestManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestManager.Cosmos
{
    public interface ICosmosDbService<TEntity>
        where TEntity: class, IModel
    {
        Task<IEnumerable<TEntity>> ListAsync(string query);
        Task<TEntity> GetAsync(string id);
        Task AddAsync(TEntity item);
        Task UpdateAsync(string id, TEntity entity);
        Task DeleteAsync(string id);
    }
}
