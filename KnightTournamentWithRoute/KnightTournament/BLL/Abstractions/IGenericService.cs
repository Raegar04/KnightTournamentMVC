using KnightTournament.Helpers;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace KnightTournament.BLL.Abstractions
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<Result<bool>> AddAsync(TEntity entity);

        Task<Result<IEnumerable<TEntity>>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null);

        Task<Result<TEntity>> GetByIdAsync(Guid id);

        Task<Result<bool>> UpdateAsync(Guid id, TEntity updatedEntity);

        Task<Result<bool>> DeleteAsync(Guid id);
    }
}
