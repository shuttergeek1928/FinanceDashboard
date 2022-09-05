using AutoMapper;
using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service.Data.DataController
{
    public class UserDataController : CommonDataController<Account>, IUserDataController
    {
        private readonly FinanceDashboardContext _context;
        private readonly IMapper _mapper;
        public UserDataController(FinanceDashboardContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Account> Update(Account entity)
        {
            _context.Account.Update(entity);

            await SaveAsync();

            return entity;
        }

        public async Task<Account> CreateUserAsync(AccountCreateModel entity)
        {
            await _context.Account.AddAsync(_mapper.Map<Account>(entity));
            await SaveAsync();
            int accountID = _dbContext.Max(x => x.AccountId);           
            Account user = _mapper.Map<Account>(entity);
            user.AccountId = accountID;
            return user;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}