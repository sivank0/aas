#region

using System.Net.NetworkInformation;

#endregion

namespace AAS.Tools.Types.IDs.UtilityId;

internal static class MacAddressHelper
{
    public static byte[] GetMacAddressBytesOrRandom(Random random)
    {
        try
        {
            NetworkInterface? networkInterface = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up &&
                            n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .OrderBy(n => n.Id)
                .FirstOrDefault();

            if (networkInterface == null) return GetRandom(random);

            return networkInterface.GetPhysicalAddress().GetAddressBytes();
        }
        catch
        {
            return GetRandom(random);
        }
    }

    private static byte[] GetRandom(Random random)
    {
        byte[] macBytes = new byte[6];
        random.NextBytes(macBytes);

        return macBytes;
    }
}