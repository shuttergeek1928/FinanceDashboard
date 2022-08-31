using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service.Data.IDataController
{
    public interface IUserDataController
    {
        Task<UserModel> Update(UserModel entity);
    }
}
