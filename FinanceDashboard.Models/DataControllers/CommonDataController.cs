using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinanceDashboard.Data.SqlServer.DataController
{
    public class CommonDataController<T> where T : class
    {
        private readonly FinanceDashboardContext _context;
        internal DbSet<T> _dbContext;

        public CommonDataController(FinanceDashboardContext context)
        {
            _context = context;
            _dbContext = _context.Set<T>();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeChildProperties = null)
        {
            IQueryable<T> entities = _dbContext;

            if (!tracked)
                entities = entities.AsNoTracking();

            if (filter != null)
                entities = entities.Where(filter);

            if (includeChildProperties != null)
            {
                foreach (var subProperty in includeChildProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    entities = entities.Include(subProperty);
                }
            }

            return await entities.FirstOrDefaultAsync();
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeChildProperties = null)
        {
            IQueryable<T> entities = _dbContext;

            if (filter != null)
                entities = entities.Where(filter);

            if (includeChildProperties != null)
            {
                foreach (var subProperty in includeChildProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    entities = entities.Include(subProperty);
                }
            }

            return await entities.ToListAsync();
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await SaveAsync();
            return entity;
        }
        public virtual async Task RemoveAsync(T entity)
        {
            _dbContext.Remove(entity);
            await SaveAsync();
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
