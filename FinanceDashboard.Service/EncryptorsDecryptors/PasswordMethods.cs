using System.Security.Cryptography;
using System.Text;

namespace FinanceDashboard.Service.EncryptorsDecryptors
{
    public class PasswordMethods : IPasswordMethods
    {
        public bool ComparePassword(string password, string existingHash, string existingSalt)
        {
            string userInputHashedPass = GetHash(password, existingSalt);
            return existingHash == userInputHashedPass;
        }

        public string GenerateSalt()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+{}[]|<>?/.,';;`-=";

            string saltString = "";

            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                while (saltString.Length != 12)
                {
                    byte[] oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (valid.Contains(character))
                    {
                        saltString += character;
                    }
                }
            }
            return saltString;

        }

        public string GetHash(string plainPassword, string salt)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(string.Concat(salt, plainPassword));

            SHA256 sha256 = SHA256Managed.Create();

            byte[] hashedByte = sha256.ComputeHash(byteArray);

            return Convert.ToBase64String(hashedByte);
        }
    }
}
