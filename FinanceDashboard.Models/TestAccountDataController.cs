using FinanceDashboard.Data.SqlServer.Entities;

namespace FinanceDashboard.Data.SqlServer.DataController
{
    public class TestAccountDataController 
    {
        private readonly FinanceDashboardContext _context;

        public TestAccountDataController(FinanceDashboardContext context)
        {
            _context = context;
        }

        public List<Subscription> Get()
        {
            IQueryable<Subscription> entities = _context.Subscription.Where(x => x.IsExpired == true);
            return entities.ToList();
        }
    }
}