using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FinanceDashboard.Data.DataControllers
{
    public class SeggmentLimitsDataContorller : CommonDataController<SegmentLimits>
    {
        private readonly FinanceDashboardContext _context;

        public SeggmentLimitsDataContorller(FinanceDashboardContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SegmentLimits> UpdateSegmentLimit(SegmentLimits entity)
        {
            _context.SegmentLimits.Update(entity);

            await SaveAsync();

            return entity;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
