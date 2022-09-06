using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Models.Account;

namespace FinanceDashboard.Service.Data.IDataController
{
    public interface IAccountDataController : ICommonDataController<Account>
    {
        Task<Account> Update(Account entity);
        Task<Account> CreateUserAsync(AccountCreateModel entity);
    }
}
