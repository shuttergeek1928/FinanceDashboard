using FinanceDashboard.Data.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDashboard.Data.DataControllers
{
    public class TestUserController
    {
        private readonly FinanceDashboardContext _context;

        public TestUserController(FinanceDashboardContext context)
        {
            _context = context;
        }

        public User ReadUser()
        {
            return _context.User.FirstOrDefault();
        }
    }
}
