using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDashboard.Core.Security
{
    public abstract class BaseController
    {
        public static FDPrincipal User => Thread.CurrentPrincipal as FDPrincipal;
    }
}
