#region

using System.Runtime.InteropServices;
using AAS.Tools.Types.IDs.UtilityId;

#endregion

namespace AAS.Tools.Types.IDs;

public class UUId : IComparable
{
    public static readonly UUId Empty = new(Guid.Empty);

    public static UUId NewUUId()
    {
        return new UUId(GenerateSequentialGuid());
    }

    private static Func<Guid> GenerateSequentialGuid;

    public static int Length => _uuid_length;

    private const string _emptyUUId = "00000000000000000000000000000000";
    private const int _uuid_bytes_length = 16;
    private const int _uuid_length = 32;
    private readonly string _uuid;

    static UUId()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            // use winapi call
            GenerateSequentialGuid = IdCreateSequentialWinApi.GetSequentialGuid;
        else
            // use managed code with unique value into process
            GenerateSequentialGuid = RFC4122Generator.GenerateTimeBasedGuid;
    }

    public UUId()
    {
        _uuid = _emptyUUId;
    }

    public UUId(string uuid)
    {
        if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid));

        if (uuid.Length != _uuid_length)
            throw new ArgumentException(
                $"The length of the String for UUID must be exactly 32({uuid.Replace("-", "").Length}) chars.",
                nameof(uuid));

        if (!IsGuid(uuid)) throw new ArgumentException("UUId must have the same characters like guid");

        _uuid = uuid.Replace("-", "");
    }

    public UUId(byte[] bytes)
    {
        if (bytes == null) throw new ArgumentNullException(nameof(bytes));

        if (bytes.Length != _uuid_bytes_length)
            throw new ArgumentException("The length of the Byte array for UUID must be exactly 16 bytes.",
                nameof(bytes));

        string val = ByteArrayToString(bytes);
        if (!IsGuid(val)) throw new ArgumentException("UUId must have the same characters like guid");

        _uuid = val;
    }

    private UUId(Guid guid)
    {
        _uuid = GetOrderedUUId(guid).ToUpper();
    }

    public override int GetHashCode()
    {
        return _uuid.GetHashCode();
    }

    public int CompareTo(object obj)
    {
        UUId uuId = obj as UUId;

        if (uuId != null) return string.Compare(_uuid, uuId.ToString(), StringComparison.Ordinal);

        throw new Exception("Ошибка сравнения объектов");
    }

    public override string ToString()
    {
        return _uuid;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((UUId)obj);
    }

    protected bool Equals(UUId other)
    {
        return string.Equals(_uuid, other._uuid);
    }

    public static bool operator ==(UUId left, UUId right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(UUId left, UUId right)
    {
        return !Equals(left, right);
    }

    public byte[] ToByteArray()
    {
        if (_uuid.Length % 2 != 0) throw new ArgumentException("hexString must have an even length");

        byte[] bytes = new byte[_uuid.Length / 2];

        for (int i = 0; i < bytes.Length; i++)
        {
            string currentHex = _uuid.Substring(i * 2, 2);
            bytes[i] = Convert.ToByte(currentHex, 16);
        }

        return bytes;
    }

    private string ByteArrayToString(byte[] bytes)
    {
        string hex = BitConverter.ToString(bytes);
        return hex.Replace("-", "");
    }

    public static bool IsGuid(string value)
    {
        Guid x;
        return Guid.TryParse(value, out x);
    }

    private static string GetOrderedUUId(Guid guid)
    {
        string g = guid.ToString();

        return string.Concat(g.Substring(24), g.Substring(19, 4), g.Substring(14, 4), g.Substring(9, 4),
            g.Substring(0, 8));
    }

    public static UUId Parse(string input)
    {
        return new UUId(input);
    }

    [Obsolete("Use Parse(string input) or TryParse(String input, out UUId result)")]
    public static UUId TryParse(string input)
    {
        if (string.IsNullOrEmpty(input)) return null;
        if (input.Length != _uuid_length) return null;

        return new UUId(input);
    }

    public static bool TryParse(string input, out UUId result)
    {
        result = null;

        if (string.IsNullOrEmpty(input)) return false;
        if (input.Length != _uuid_length) return false;

        result = new UUId(input);

        return true;
    }
}