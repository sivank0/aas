using AAS.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAS.Services.Bids.Repositories.Queries;

public class Sql
{
    public static String Bids_Save => SqlFileProvider.GetQuery(folder: "Bids");

    public static String Bids_GetAll => SqlFileProvider.GetQuery(folder: "Bids");

}
