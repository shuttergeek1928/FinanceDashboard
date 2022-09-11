using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FinanceDashboard.Data.DataController
{
    public class SubscriptionDataController : CommonDataController<Subscription>
    {
        private readonly FinanceDashboardContext _context;
        public SubscriptionDataController(FinanceDashboardContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Subscription> UpdateSubscription(Subscription entity)
        {
            _context.Subscription.Update(entity);

            await SaveAsync();

            return entity;
        }

        public async Task<Subscription> PatchSubscription(JsonPatchDocument<Subscription> sub, Guid id)
        {
            var subscription = await _context.Subscription.FindAsync(id);

            if (subscription == null)
                return null;

            sub.ApplyTo(subscription);
            subscription = await UpdateSubscription(subscription);
            await SaveAsync();
            return subscription;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
