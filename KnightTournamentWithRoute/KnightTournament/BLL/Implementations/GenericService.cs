using KnightTournament.BLL.Abstractions;
using KnightTournament.DAL;
using KnightTournament.Extensions;
using KnightTournament.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Reflection;

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
            var res = await _repository.GetAllAsync(filter);
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

        public virtual async Task<Result<IEnumerable<TEntity>>> SearchAsync(string propertyName, string value, Expression<Func<TEntity, bool>>? filter = null)
        {
            var result = await _repository.GetAllAsync(filter);
            if (value is null)
            {
                return result;
            }
            var entityType = typeof(TEntity);
            if (propertyName == "All")
            {
                return await SearchByAllAsync(value, filter);
            }
            var property = entityType.GetProperty(propertyName);

            var entities = new List<TEntity>();
            foreach (var entity in result.Data)
            {
                var propValue = property.GetValue(entity).ToString();
                if (propValue.Contains(value))
                {
                    entities.Add(entity);
                }

            }

            return new Result<IEnumerable<TEntity>>(true, entities);
        }

        private async Task<Result<IEnumerable<TEntity>>> SearchByAllAsync(string value, Expression<Func<TEntity, bool>>? filter = null)
        {
            var entityType = typeof(TEntity);
            var result = await _repository.GetAllAsync(filter);
            var entities = new List<TEntity>();
            foreach (var property in entityType.GetProperties())
            {
                if (!property.GetGetMethod().IsVirtual)
                {
                    foreach (var entity in result.Data)
                    {
                        var propValue = property.GetValue(entity).ToString();
                        if (propValue.Contains(value))
                        {
                            entities.Add(entity);
                        }

                    }
                }

            }

            return new Result<IEnumerable<TEntity>>(true, entities);
        }

        public async Task<Result<IEnumerable<TEntity>>> FilterAsync(string propertyName, string valueFrom, string valueTo, Expression<Func<TEntity, bool>>? filter = null)
        {
            var entityType = typeof(TEntity);
            var result = await _repository.GetAllAsync(filter);
            var property = entityType.GetProperty(propertyName);
            if (valueFrom is null && valueTo is null)
            {
                return result;
            }

            var entities = new List<TEntity>();
            foreach (var entity in result.Data)
            {
                var propValue = property.GetValue(entity).ToString();
                if (propValue.CompareObjectsExtension(valueFrom, valueTo))
                {
                    entities.Add(entity);
                }

            }

            return new Result<IEnumerable<TEntity>>(true, entities);
        }
    }
}
