using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDashboard.Models.Models.SegmentLimits
{
    public class SegmentLimitsCreateModel
    {
        public decimal SubscriptionLimit { get; set; }
        public decimal EmiLimit { get; set; }
    }
}
