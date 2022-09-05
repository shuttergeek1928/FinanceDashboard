using FinanceDashboard.Data.SqlServer.Entities;

namespace FinanceDashboard.Service.Data.IDataController
{
    public interface ISucbscriptionDataController : ICommonDataController<Subscription>
    {
        Task<Subscription> UpdateSubscription(Subscription entity);
        Task SaveAsync();
    }
}
