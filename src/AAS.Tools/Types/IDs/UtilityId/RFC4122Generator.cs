namespace AAS.Tools.Types.IDs.UtilityId;

internal static class RFC4122Generator
{
    // number of bytes in guid
    public const int ByteArraySize = 16;

    // multiplex variant info
    public const int VariantByte = 8;
    public const int VariantByteMask = 0x3f;
    public const int VariantByteShift = 0x80;

    // multiplex version info
    public const int VersionByte = 7;
    public const int VersionByteMask = 0x0f;
    public const int VersionByteShift = 4;

    // indexes within the id array for certain boundaries
    private const byte TIMESTAMP_BYTE = 0;
    private const byte GUID_CLOCK_SEQUENCE_BYTE = 8;
    private const byte NODE_BYTE = 10;

    // offset to move from 1/1/0001, which is 0-time for .NET, to gregorian 0-time of 10/15/1582
    private static readonly DateTimeOffset GregorianCalendarStart = new(1582, 10, 15, 0, 0, 0, TimeSpan.Zero);

    // random clock sequence and node
    private static int _defaultClockSequence;
    public static byte[] DefaultNode { get; set; }

    static RFC4122Generator()
    {
        Random random = new Random();

        _defaultClockSequence = random.Next();
        DefaultNode = MacAddressHelper.GetMacAddressBytesOrRandom(random);
    }

    public static GuidVersion GetVersion(this Guid guid)
    {
        byte[] bytes = guid.ToByteArray();

        return (GuidVersion)((bytes[VersionByte] & 0xFF) >> VersionByteShift);
    }

    public static DateTimeOffset GetDateTimeOffset(Guid guid)
    {
        byte[] bytes = guid.ToByteArray();

        // reverse the version
        bytes[VersionByte] &= (byte)VersionByteMask;
        bytes[VersionByte] |= (byte)((byte)GuidVersion.TimeBased >> VersionByteShift);

        byte[] timestampBytes = new byte[8];
        Array.Copy(bytes, TIMESTAMP_BYTE, timestampBytes, 0, 8);

        long timestamp = BitConverter.ToInt64(timestampBytes, 0);
        long ticks = timestamp + GregorianCalendarStart.Ticks;

        return new DateTimeOffset(ticks, TimeSpan.Zero);
    }

    public static DateTime GetDateTime(Guid guid)
    {
        return GetDateTimeOffset(guid).DateTime;
    }

    public static DateTime GetLocalDateTime(Guid guid)
    {
        return GetDateTimeOffset(guid).LocalDateTime;
    }

    public static DateTime GetUtcDateTime(Guid guid)
    {
        return GetDateTimeOffset(guid).UtcDateTime;
    }

    public static Guid GenerateTimeBasedGuid()
    {
        return GenerateTimeBasedGuid(DateTimeOffset.UtcNow, DefaultNode);
    }

    public static Guid GenerateTimeBasedGuid(DateTimeOffset dateTime, byte[] node)
    {
        if (node == null) throw new ArgumentNullException("node");

        if (node.Length != 6) throw new ArgumentOutOfRangeException("node", "The node must be 6 bytes.");

        long ticks = (dateTime - GregorianCalendarStart).Ticks;
        byte[] guid = new byte[ByteArraySize];
        byte[] timestamp = BitConverter.GetBytes(ticks);

        // copy node
        Array.Copy(node, 0, guid, NODE_BYTE, Math.Min(6, node.Length));

        // copy clock sequence
        byte[] clockSequenceBytes =
            BitConverter.GetBytes(Interlocked.Increment(ref _defaultClockSequence)).Reverse().ToArray();

        Array.Copy(clockSequenceBytes, 2, guid, GUID_CLOCK_SEQUENCE_BYTE, 2);

        // copy timestamp
        Array.Copy(timestamp, 0, guid, TIMESTAMP_BYTE, Math.Min(8, timestamp.Length));

        // set the variant
        guid[VariantByte] &= (byte)VariantByteMask;
        guid[VariantByte] |= (byte)VariantByteShift;

        // set the version
        guid[VersionByte] &= (byte)VersionByteMask;
        guid[VersionByte] |= (byte)((byte)GuidVersion.TimeBased << VersionByteShift);

        return new Guid(guid);
    }
}

// guid version types
public enum GuidVersion
{
    TimeBased = 0x01,
    Reserved = 0x02,
    NameBased = 0x03,
    Random = 0x04
}