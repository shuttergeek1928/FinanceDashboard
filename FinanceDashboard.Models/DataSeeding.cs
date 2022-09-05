using FinanceDashboard.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDashboard.Data.SqlServer
{
    public class DataSeeding
    {
        public static Account SeedAccount
        {
            get
            {
                return new Account()
                {
                    AccountId = 1,
                    Id = Guid.NewGuid(),
                    Name = "Atishay Vishwakarma",
                    FirstName = "Atishay",
                    LastName = "Vishwakarma",
                    Email = "atishay1928@outlook.com",
                    PasswordHash = "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=",
                    HashingSalt = "@6ZD3aazp-zp",
                    PasswordHashHistory = "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=,",
                    MobileNumber = "9827766387",
                    CreatedOn = new DateTime(2022,01,01)
                };
            }
        }
        public static Subscription SeedSubscription
        {
            get
            {
                return new Subscription()
                {
                    Id = Guid.NewGuid(),
                    AccountId = 1,
                    SubscriptionName = "Netflix",
                    SubscribedOnEmail = "atishay1928@outlook.com",
                    SubscribedOnMobileNumber = "9827766387",
                    Password = "Password1!",
                    BillingDate = new DateTime(2022, 01, 01),
                    RenewalDate = new DateTime(2022, 01, 01).AddMonths(1),
                    RenewalCycle = 1,
                    Amount = 500,
                    RenewalAmount = 350,
                };
            }
        }
    }
}
