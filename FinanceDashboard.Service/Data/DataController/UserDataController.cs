using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service.Data.DataController
{
    public class UserDataController : CommonDataController<UserModel>, IUserDataController
    {
        private readonly FinanceDashboardContext _context;
        public UserDataController(FinanceDashboardContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserModel> Update(UserModel entity)
        {
            _context.Update(entity);
            
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
