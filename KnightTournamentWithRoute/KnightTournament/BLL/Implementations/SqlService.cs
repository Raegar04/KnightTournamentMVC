using KnightTournament.BLL.Abstractions;
using KnightTournament.DAL;
using KnightTournament.Helpers;
using Microsoft.EntityFrameworkCore;

namespace KnightTournament.BLL.Implementations
{
    public class SqlService<TEntity> : ISqlService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly DbSet<TEntity> _entities;

        public SqlService(UnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<TEntity>();
            _entities = _repository.context.Set<TEntity>();
        }

        public async Task<Result<ICollection<TEntity>>> GetByQuery(string query)
        {

            try
            {
                var entities = await _entities.FromSqlRaw(query).ToListAsync();
                if (entities.Count == 0)
                {
                    return new Result<ICollection<TEntity>>(false, "There are no matching items");
                }

                return new Result<ICollection<TEntity>>(true, entities);
            }
            catch
            {
                return new Result<ICollection<TEntity>>(false, "Cannot get items");
            }
        }
    }
}
