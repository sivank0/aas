using AAS.Tools.Converters;
using Newtonsoft.Json;

namespace AAS.Tools.Types.IDs;

[Serializable]
[JsonConverter(typeof(IDJsonConverter))]
public readonly struct ID : IComparable, IEquatable<ID>
{
	public static readonly ID Empty = new ID(UUId.Empty);

	private readonly UUId _id;

	public static ID New() => new ID(UUId.NewUUId());

	public ID(String id)
	{
		_id = new UUId(id);
	}

	private ID(UUId id)
	{
		_id = id;
	}

	public ID(Byte[] values)
	{
		_id = new UUId(values);
	}

	public Int32 CompareTo(Object? obj) => obj switch
	{
		ID id => CompareTo(id),
		_ => throw new InvalidCastException(nameof(obj) + " is not " + nameof(ID)),
	};

	public Int32 CompareTo(ID obj)
	{
		return _id.CompareTo(obj._id);
	}

	public override String ToString()
	{
		return _id.ToString();
	}

	public override Boolean Equals(Object? obj)
	{
		return obj is ID id && Equals(id);
	}

	public Boolean Equals(ID other)
	{
		return _id.Equals(other._id);
	}

	public override Int32 GetHashCode()
	{
		return _id.GetHashCode();
	}

	public static ID Parse(String s)
	{
		return new(s);
	}

	public static Boolean TryParse(String s, out ID id)
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
	public Byte[] ToByteArray() => _id.ToByteArray();

	public static Boolean operator ==(ID? left, ID? right) => left is null ? right is null : left.Equals(right);
	public static Boolean operator !=(ID? left, ID? right) => !(left == right);
}
