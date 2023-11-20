using KnightTournament.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KnightTournament.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public ApplicationDbContext context { get; set; }
        public DbSet<TEntity> dbSet { get; set; }
        Task<Result<IEnumerable<TEntity>>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<Result<TEntity>> GetByIdAsync(Guid id);

        Task<Result<bool>> AddItemAsync(TEntity entity);

        Task<Result<bool>> DeleteItemAsync(Guid id);


        Result<bool> UpdateItemAsync(Guid id, TEntity entityToUpdate);
    }
}
