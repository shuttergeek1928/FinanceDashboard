using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;

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
            _context.Update(entity);

            await this.SaveAsync();

            return entity;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
