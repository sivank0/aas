using System.Security.Cryptography;
using System.Text;

namespace AAS.Tools.Managers;

public class HashManager
{
    public static String? DefinePasswordHash(String? password)
    {
        if (String.IsNullOrWhiteSpace(password)) return null;

        Byte[] bytes = Encoding.Unicode.GetBytes(password);
        MD5CryptoServiceProvider cryptoService = new MD5CryptoServiceProvider(); // REFACTORING MD5CryptoServiceProvider is obsolete
        Byte[] byteHash = cryptoService.ComputeHash(bytes);
        String hash = String.Empty;

        foreach (Byte b in byteHash) hash += $"{b:x2}";

        return hash;
    }
}
