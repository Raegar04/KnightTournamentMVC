
using KnightTournament.Attributes;
using KnightTournament.DAL;

namespace KnightTournament.Helpers
{
    public class ValidationHelper
    {
        public async Task<Result<bool>> ValidateObject<TEntity>(IRepository<TEntity> repository, TEntity entity) where TEntity : class
        {
            if (repository is null || entity is null)
            {
                return new Result<bool>(false, "Invalid input data");
            }

            var entityType = typeof(TEntity);
            var entities = (await repository.GetAllAsync()).Data;
            foreach (var property in entityType.GetProperties())
            {
                var value = property.GetValue(entity);
                if ((property.IsDefined(typeof(DisallowSimilarAttribute), false) && entities.Any(item=>property?.GetValue(item).ToString()==value?.ToString())))
                {
                    return new Result<bool>(false, $"{property.Name} is not valid");
                }
            }

            return new Result<bool>(true);
        }
    }
}
