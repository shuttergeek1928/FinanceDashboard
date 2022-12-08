using System.Security.Principal;

namespace FinanceDashboard.Core.Security
{
    public class FDIdentity : IIdentity
    {
        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }

        public int AccountId { get; set; }

        public string Email { get; set; }

        public string SessionToken { get; set; }

        public FDIdentity(string authenticationType, bool isAuthenticated, string name, int accountId, string email, string sessionToken)
        {
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            Name = name;
            AccountId = accountId;
            Email = email;
            SessionToken = sessionToken;
        }
    }
}
