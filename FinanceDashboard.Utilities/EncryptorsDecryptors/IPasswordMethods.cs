namespace FinanceDashboard.Utilities.EncryptorsDecryptors
{
    public interface IPasswordMethods
    {
        public string GenerateSalt();
        public string GetHash(string plainPassword, string salt);
        public bool ComparePassword(string password, string existingHash, string existingSalt);
    }
}