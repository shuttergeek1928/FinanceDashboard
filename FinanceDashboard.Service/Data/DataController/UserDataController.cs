using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Service.Data.DataController
{
    public class UserDataController : CommonDataController<User>, IUserDataController
    {
        private readonly FinanceDashboardContext _context;
        public UserDataController(FinanceDashboardContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> Update(User entity)
        {
            _dbContext.Update(entity);

            await SaveAsync();

            return entity;
        }

        public override async Task<User> CreateAsync(User entity)
        {
            await _context.User.AddAsync(entity);
            await SaveAsync();
            int accountID = _dbContext.Max(x => x.AccountId);
            entity.AccountId = accountID;
            return entity;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
