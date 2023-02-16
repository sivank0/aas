using System.Net;
using Microsoft.AspNetCore.Http;

namespace PMS.Tools.Managers;

public static class CookieManager
{
	/// <summary>
	/// Запись в Cookie
	/// </summary>
	public static void Write(HttpResponse response, Cookie cookie, DateTime expires)
	{
		CookieOptions options = new CookieOptions();
		options.Expires = expires;

		response.Cookies.Append(cookie.Name, cookie.Value, options);
	}

	/// <summary>
	/// Чтение из Cookie
	/// </summary>
	public static String? Read(HttpRequest request, String cookie)
	{
		request.Cookies.TryGetValue(cookie, out String? value);
		return value;
	}

	/// <summary>
	/// Удаление из Cookie
	/// </summary>
	public static void Delete(HttpResponse response, String cookie)
	{
		response.Cookies.Delete(cookie);
	}
}
