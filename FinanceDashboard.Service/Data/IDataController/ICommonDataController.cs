using System.Linq.Expressions;

namespace FinanceDashboard.Service.Data.IDataController
{
    public interface ICommonDataController<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeChildProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeChildProperties = null);
        Task<T> CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
