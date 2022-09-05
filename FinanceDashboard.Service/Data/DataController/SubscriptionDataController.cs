using AutoMapper;
using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Data.IDataController;

namespace FinanceDashboard.Service.Data.DataController
{
    public class SubscriptionDataController : CommonDataController<Subscription>, ISucbscriptionDataController
    {
        private readonly FinanceDashboardContext _context;
        private readonly IMapper _mapper;
        public SubscriptionDataController(FinanceDashboardContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Subscription> UpdateSubscription(Subscription entity)
        {
           _context.Subscription.Update(entity);

            await SaveAsync();
            
            return entity;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
