using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FinanceDashboard.Data.DataControllers
{
    public class EmiDataContoller : CommonDataController<EMI>
    {
        private readonly FinanceDashboardContext _context;

        public EmiDataContoller(FinanceDashboardContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EMI> UpdateEmi(EMI entity)
        {
            _context.Emi.Update(entity);

            await SaveAsync();

            return entity;
        }

        public async Task<EMI> PatchEmi(JsonPatchDocument<EMI> sub, Guid id)
        {
            var emi = await _context.Emi.FindAsync(id);

            if (emi == null)
                return null;

            sub.ApplyTo(emi);
            emi = await UpdateEmi(emi);
            await SaveAsync();
            return emi;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
