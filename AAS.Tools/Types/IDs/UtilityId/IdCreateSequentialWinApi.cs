using System;
using System.Runtime.InteropServices;

namespace PMS.Tools.Types.IDs.UtilityId
{
    internal static class IdCreateSequentialWinApi
    {
		[DllImport("rpcrt4.dll", SetLastError = true)]
		private static extern Int32 UuidCreateSequential(out Guid guid);

		public static Guid GetSequentialGuid()
		{
			const Int32 RPC_S_OK = 0;
			Int32 rpcResult = UuidCreateSequential(out Guid guid);
			if (rpcResult != RPC_S_OK) guid = Guid.NewGuid();

			return guid;
		}
	}
}
