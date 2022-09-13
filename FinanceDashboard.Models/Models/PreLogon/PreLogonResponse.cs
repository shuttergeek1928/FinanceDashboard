using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDashboard.Models.Models.PreLogon
{
    public class PreLogonResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
