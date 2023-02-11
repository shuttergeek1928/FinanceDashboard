using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FinanceDashboard.Data.DataControllers
{
    public class IncomeDataContoller : CommonDataController<Income>
    {
        private readonly FinanceDashboardContext _context;

        public IncomeDataContoller(FinanceDashboardContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Income> UpdateIncome(Income entity)
        {
            _context.Income.Update(entity);

            await SaveAsync();

            return entity;
        }

        public async Task<Income> PatchIncome(JsonPatchDocument<Income> sub, Guid id)
        {
            var income = await _context.Income.FindAsync(id);

            if (income == null)
                return null;

            sub.ApplyTo(income);
            income = await UpdateIncome(income);
            await SaveAsync();
            return income;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
