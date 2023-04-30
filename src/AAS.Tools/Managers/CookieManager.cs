#region

using System.Net;
using Microsoft.AspNetCore.Http;

#endregion

namespace PMS.Tools.Managers;

public static class CookieManager
{
	/// <summary>
	///     Запись в Cookie
	/// </summary>
	public static void Write(HttpResponse response, Cookie cookie, DateTime expires)
    {
        CookieOptions options = new CookieOptions();
        options.Expires = expires;

        response.Cookies.Append(cookie.Name, cookie.Value, options);
    }

	/// <summary>
	///     Чтение из Cookie
	/// </summary>
	public static string? Read(HttpRequest request, string cookie)
    {
        request.Cookies.TryGetValue(cookie, out string? value);
        return value;
    }

	/// <summary>
	///     Удаление из Cookie
	/// </summary>
	public static void Delete(HttpResponse response, string cookie)
    {
        response.Cookies.Delete(cookie);
    }
}