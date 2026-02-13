using System.Security.Cryptography;
using System.Text;

namespace OrdersSecurity;

public static class PasswordHashing
{
    // VULNERABLE: fast hash (not for password storage)
    public static string HashPasswordBad(string password)
    {
        using var sha1 = SHA1.Create();
        return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    // SAFE: PBKDF2 (built-in)
    public static string HashPasswordPbkdf2(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 150_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }
}
