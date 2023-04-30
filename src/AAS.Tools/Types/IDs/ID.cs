#region

using AAS.Tools.Converters;
using Newtonsoft.Json;

#endregion

namespace AAS.Tools.Types.IDs;

[Serializable]
[JsonConverter(typeof(IDJsonConverter))]
public readonly struct ID : IComparable, IEquatable<ID>
{
    public static readonly ID Empty = new(UUId.Empty);

    private readonly UUId _id;

    public static ID New()
    {
        return new ID(UUId.NewUUId());
    }

    public ID(string id)
    {
        _id = new UUId(id);
    }

    private ID(UUId id)
    {
        _id = id;
    }

    public ID(byte[] values)
    {
        _id = new UUId(values);
    }

    public int CompareTo(object? obj)
    {
        return obj switch
        {
            ID id => CompareTo(id),
            _ => throw new InvalidCastException(nameof(obj) + " is not " + nameof(ID))
        };
    }

    public int CompareTo(ID obj)
    {
        return _id.CompareTo(obj._id);
    }

    public override string ToString()
    {
        return _id.ToString();
    }

    public override bool Equals(object? obj)
    {
        return obj is ID id && Equals(id);
    }

    public bool Equals(ID other)
    {
        return _id.Equals(other._id);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    public static ID Parse(string s)
    {
        return new ID(s);
    }

    public static bool TryParse(string s, out ID id)
    {
        try
        {
            id = Parse(s);
            return true;
        }
        catch
        {
            id = default;
            return false;
        }
    }

    public byte[] ToByteArray()
    {
        return _id.ToByteArray();
    }

    public static bool operator ==(ID? left, ID? right)
    {
        return left is null ? right is null : left.Equals(right);
    }

    public static bool operator !=(ID? left, ID? right)
    {
        return !(left == right);
    }
}