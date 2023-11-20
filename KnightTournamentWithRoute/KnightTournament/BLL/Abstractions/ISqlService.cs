using KnightTournament.Helpers;

namespace KnightTournament.BLL.Abstractions
{
    public interface ISqlService<TEntity> where TEntity : class
    {
        Task<Result<ICollection<TEntity>>> GetByQuery(string query);
    }
}
