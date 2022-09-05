using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service.Data.IDataController
{
    public interface IUserDataController : ICommonDataController<Account>
    {
        Task<Account> Update(Account entity);
        Task<Account> CreateUserAsync(AccountCreateModel entity);
    }
}
