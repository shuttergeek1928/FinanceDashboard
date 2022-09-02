using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service.Data.IDataController
{
    public interface IUserDataController : ICommonDataController<User>
    {
        Task<User> Update(User entity);
        Task<User> CreateUserAsync(UserCreateModel entity);
    }
}
