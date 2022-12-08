using System.Security.Principal;

namespace FinanceDashboard.Core.Security
{
    internal class FDPrincipal : IPrincipal
    {
        readonly FDIdentity _identity;

        public FDPrincipal(string name, int accountId, string email, string sessionToken)
        {
            _identity = new FDIdentity("FDAuth", true, name, accountId, email, sessionToken);
        }
        public IIdentity? Identity 
        { 
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
