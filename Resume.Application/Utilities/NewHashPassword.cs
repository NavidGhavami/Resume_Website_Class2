using System.Security.Cryptography;

namespace Resume.Application.Utilities
{
    public static class NewHashPassword
    {
        public static string HashPassword(string password, string salt)
        {
            // Create a Rfc2898DeriveBytes object 
            // Adjust iterations count for desired security level (e.g., 10000+)
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(32));
            }
        }

        public static string GenerateSalt()
        {
            // Generate a cryptographically secure random salt
            byte[] randomBytes = new byte[24];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}
