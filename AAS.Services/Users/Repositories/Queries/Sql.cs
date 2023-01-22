using AAS.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAS.Services.Users.Repositories.Queries;
internal static class Sql
{
    #region Users

    public static String Users_Save => SqlFileProvider.GetQuery(folder: "Users");

    #endregion
}
