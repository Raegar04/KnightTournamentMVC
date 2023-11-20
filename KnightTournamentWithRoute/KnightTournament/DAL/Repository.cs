using KnightTournament.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KnightTournament.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ApplicationDbContext context { get; set; }
        public DbSet<TEntity> dbSet { get; set; }

        public Repository(ApplicationDbContext appManagerContext)
        {
            context = appManagerContext;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<Result<IEnumerable<TEntity>>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                             (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return new Result<IEnumerable<TEntity>>(true, await orderBy(query).ToListAsync());
                }
                else
                {
                    return new Result<IEnumerable<TEntity>>(true, await query.ToListAsync());
                }
            }
            catch
            {
                return new Result<IEnumerable<TEntity>>(false, "Cannot get item");
            }

        }


        public virtual async Task<Result<TEntity>> GetByIdAsync(Guid id)
        {
            var result = await dbSet.FindAsync(id);
            if (result is null)
            {
                return new Result<TEntity>(false, "Cannot find items");
            }
            return new Result<TEntity>(true, result);
        }

        public virtual async Task<Result<bool>> AddItemAsync(TEntity entity)
        {

            try
            {
                await dbSet.AddAsync(entity);
                await context.SaveChangesAsync();
                return new Result<bool>(true);
            }
            catch
            {
                return new Result<bool>(false, "Cannot add item");
            }
        }

        public async Task<Result<bool>> DeleteItemAsync(Guid id)
        {
            try
            {
                var entityToDelete = await dbSet.FindAsync(id);
                Delete(entityToDelete);
                await context.SaveChangesAsync();
                return new Result<bool>(true);
            }
            catch (NullReferenceException)
            {
                return new Result<bool>(false, "Cannot find the item with this id");
            }
            catch
            {
                return new Result<bool>(false, "Cannot delete the item");
            }
        }

        private void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual Result<bool> UpdateItemAsync(Guid id, TEntity entityToUpdate)
        {
            try
            {
                var existing = dbSet.Find(id);
                if (existing != null) 
                {
                    context.Entry(existing).State = EntityState.Detached;
                    context.Entry(entityToUpdate).State = EntityState.Modified; 
                    context.SaveChanges();
                }
                //context.Update(entityToUpdate);
                //context.SaveChanges();
                return new Result<bool>(true);
            }
            catch
            {
                return new Result<bool>(false, "Cannot update item");
            }
        }
    }
}
