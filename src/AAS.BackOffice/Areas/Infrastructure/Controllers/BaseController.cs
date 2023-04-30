#region

using AAS.BackOffice.Infrastructure;
using AAS.BackOffice.Infrastructure.Enum;
using AAS.BackOffice.Infrastructure.ReactApp;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Extensions;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace AAS.BackOffice.Controllers;

public class BaseController : Controller
{
    private SystemUser? _systemUser => (SystemUser?)HttpContext.Items[CookieNames.SystemUser];
    protected SystemUser SystemUser => _systemUser!;

    public ViewResult ReactApp()
    {
        bool isMobile = HttpContext.Request.IsMobileBrowser();

        ReactApp app = new ReactApp(isMobile ? "mobile" : "desktop",
            isMobile ? BrowserType.Mobile : BrowserType.Desktop);

        if (_systemUser is not null)
        {
            app.WithSystemUser(_systemUser);
            app.WithSidebar(_systemUser);
        }

        return View("ReactApp", app);
    }
}