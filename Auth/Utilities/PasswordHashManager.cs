using System.Security.Cryptography;
using System.Text;

namespace Auth.Utilities;

public static class PasswordHashManager
{
    public static string HashPasword(string password)
    {

        using (var sha256 = SHA256.Create())
        {
            return Encoding.UTF8.GetString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }

    public static bool VerifyPasswordHash(string password, string hashedPassword)
    {
        using (var sha256 = SHA256.Create())
        {
            string newHashed = Encoding.UTF8.GetString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return newHashed == hashedPassword;
        }
    }
}