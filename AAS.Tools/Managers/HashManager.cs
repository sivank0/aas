using System.Security.Cryptography;

namespace AAS.Tools.Managers;

public class HashManager
{
    private const Int32 _saltSize = 16; // 128 bits
    private const Int32 _keySize = 32; // 256 bits
    private const Int32 _iterations = 100000;
    private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

    private const Char segmentDelimiter = ':';

    public static String Hash(string input)
    {
        Byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
        Byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            _iterations,
            _algorithm,
            _keySize
        );
        return String.Join(
            segmentDelimiter,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt),
            _iterations,
            _algorithm
        );
    }

    public static Boolean Verify(string input, string hashString)
    {
        String[] segments = hashString.Split(segmentDelimiter);
        Byte[] hash = Convert.FromHexString(segments[0]);
        Byte[] salt = Convert.FromHexString(segments[1]);
        Int32 iterations = int.Parse(segments[2]);
        HashAlgorithmName algorithm = new HashAlgorithmName(segments[3]);
        Byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            iterations,
            algorithm,
            hash.Length
        );
        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}
