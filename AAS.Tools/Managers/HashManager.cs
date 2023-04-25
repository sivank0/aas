#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace AAS.Tools.Managers;

public class HashManager
{
    public static string? DefinePasswordHash(string? password)
    {
        if (string.IsNullOrWhiteSpace(password)) return null;

        byte[] bytes = Encoding.Unicode.GetBytes(password);
        MD5CryptoServiceProvider
            cryptoService = new MD5CryptoServiceProvider(); // REFACTORING MD5CryptoServiceProvider is obsolete
        byte[] byteHash = cryptoService.ComputeHash(bytes);
        string hash = string.Empty;

        foreach (byte b in byteHash) hash += $"{b:x2}";

        return hash;
    }
}