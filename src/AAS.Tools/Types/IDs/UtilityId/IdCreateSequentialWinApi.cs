#region

using System.Runtime.InteropServices;

#endregion

namespace AAS.Tools.Types.IDs.UtilityId;

internal static class IdCreateSequentialWinApi
{
    [DllImport("rpcrt4.dll", SetLastError = true)]
    private static extern int UuidCreateSequential(out Guid guid);

    public static Guid GetSequentialGuid()
    {
        const int RPC_S_OK = 0;
        int rpcResult = UuidCreateSequential(out Guid guid);
        if (rpcResult != RPC_S_OK) guid = Guid.NewGuid();

        return guid;
    }
}