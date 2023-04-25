#region

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

#endregion

namespace AAS.Tools.Extensions;

public static class RequestExtensions
{
    private static readonly Regex Mobiles =
        new(
            @"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino",
            RegexOptions.IgnoreCase | RegexOptions.Multiline);

    private static readonly Regex Tablets = new("ipad|android|android 3.0|xoom|sch-i800|playbook|tablet|kindle|nexus",
        RegexOptions.IgnoreCase | RegexOptions.Multiline);

    public static bool IsAjaxRequest(this HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Headers != null)
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";

        return false;
    }

    public static bool IsMobileBrowser(this HttpRequest request)
    {
        try
        {
            string userAgent = request.UserAgent();
            return Mobiles.IsMatch(userAgent) || Tablets.IsMatch(userAgent);
        }
        catch
        {
            return false;
        }
    }


    public static string UserAgent(this HttpRequest request)
    {
        return request.Headers["User-Agent"];
    }
}