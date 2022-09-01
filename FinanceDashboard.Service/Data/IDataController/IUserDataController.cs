using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service.Data.IDataController
{
    public interface IUserDataController : ICommonDataController<User>
    {
        Task<User> Update(User entity);
    }
}
