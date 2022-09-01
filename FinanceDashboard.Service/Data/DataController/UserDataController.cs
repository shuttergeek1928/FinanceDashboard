using AutoMapper;
using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Models;
using FinanceDashboard.Models.Data.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Service.Data.DataController
{
    public class UserDataController : CommonDataController<User>, IUserDataController
    {
        private readonly FinanceDashboardContext _context;
        private readonly IMapper _mapper;
        public UserDataController(FinanceDashboardContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> Update(User entity)
        {
            _dbContext.Update(entity);

            await SaveAsync();

            return entity;
        }

        public async Task<User> CreateUserAsync(UserCreateModel entity)
        {
            await _context.User.AddAsync(_mapper.Map<User>(entity));
            await SaveAsync();
            int accountID = _dbContext.Max(x => x.AccountId);           
            User user = _mapper.Map<User>(entity);
            user.AccountId = accountID;
            return user;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
