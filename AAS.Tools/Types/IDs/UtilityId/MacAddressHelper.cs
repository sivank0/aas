using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace PMS.Tools.Types.IDs.UtilityId
{
    internal static class MacAddressHelper
    {
        public static Byte[] GetMacAddressBytesOrRandom(Random random)
        {
            try
            {
                NetworkInterface? networkInterface = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .OrderBy(n => n.Id)
                    .FirstOrDefault();

                if (networkInterface == null)
                {
                    return GetRandom(random);
                }

                return networkInterface.GetPhysicalAddress().GetAddressBytes();
            }
            catch
            {
                return GetRandom(random);
            }
        }

        private static Byte[] GetRandom(Random random)
        {
            Byte[] macBytes = new Byte[6];
            random.NextBytes(macBytes);

            return macBytes;
        }
    }
}
