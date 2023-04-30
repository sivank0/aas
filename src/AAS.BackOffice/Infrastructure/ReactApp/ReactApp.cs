#region

using AAS.BackOffice.Areas.Users.Models;
using AAS.BackOffice.Infrastructure.Enum;
using AAS.BackOffice.Infrastructure.Sidebars;
using AAS.Domain.Users.SystemUsers;

#endregion

namespace AAS.BackOffice.Infrastructure.ReactApp;

public class ReactApp
{
    public string Name { get; }
    public string ContainerId { get; }

    public BackOfficeSystemUser? SystemUser { get; set; }
    public BrowserType BrowserType { get; }
    public SidebarItem[] SidebarLinksTree { get; private set; }
    public BaseConfig BaseConfig { get; }

    public ReactApp(string name, BrowserType browserType, string containerId = "app")
    {
        Name = name;
        ContainerId = containerId;
        SystemUser = null;
        SidebarLinksTree = new SidebarItem[0];
        BaseConfig = BaseConfig.Current;
        BrowserType = browserType;
    }

    public ReactApp WithSystemUser(SystemUser systemUser)
    {
        SystemUser = new BackOfficeSystemUser(systemUser);
        return this;
    }

    public ReactApp WithSidebar(SystemUser? systemUser)
    {
        SidebarLinksTree = Sidebar.GetLinksTree(systemUser);
        return this;
    }
}