using KnightTournament.BLL.Abstractions;
using KnightTournament.DAL;
using KnightTournament.Helpers;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace KnightTournament.BLL.Implementations
{
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected IRepository<TEntity>? _repository;
        private readonly ValidationHelper _validationHelper = new ValidationHelper();

        public virtual async Task<Result<bool>> AddAsync(TEntity entity)
        {
            var validResult = await _validationHelper.ValidateObject(_repository, entity);
            if (!validResult.IsSuccessful)
            {
                return validResult;
            }

            return await _repository.AddItemAsync(entity);
        }

        public virtual async Task<Result<bool>> DeleteAsync(Guid id)
        {
            return await _repository.DeleteItemAsync(id);
        }


        public virtual async Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            var res =  await _repository.GetAllAsync(filter);
            if (!res.IsSuccessful)
            {
                return new Result<IEnumerable<TEntity>>(true, new List<TEntity>());
            }
            return res;
        }

        public virtual async Task<Result<TEntity>> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<Result<bool>> UpdateAsync(Guid id, TEntity updatedEntity)
        {
            var validResult = await _validationHelper.ValidateObject(_repository, updatedEntity);
            if (!validResult.IsSuccessful)
            {
                return validResult;
            }

            return _repository.UpdateItemAsync(id, updatedEntity);
        }
    }
}
