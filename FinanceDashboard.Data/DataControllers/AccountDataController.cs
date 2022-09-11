using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Entities;

namespace FinanceDashboard.Data.DataController
{
    public class AccountDataController : CommonDataController<Account>
    {
        private readonly FinanceDashboardContext _context;
        public AccountDataController(FinanceDashboardContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account> Update(Account entity)
        {
            _context.Account.Update(entity);

            await SaveAsync();

            return entity;
        }

        public async Task<Account> CreateUserAsync(Account entity)
        {
            await _context.Account.AddAsync(entity);
            await SaveAsync();
            int accountID = _dbContext.Max(x => x.AccountId);
            entity.AccountId = accountID;
            return entity;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}