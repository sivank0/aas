using AAS.BackOffice.Areas.Users.Models;
using AAS.BackOffice.Infrastructure.Sidebars;
using AAS.BackOffice.Infrastructure.Enum;
using AAS.Domain.Users.SystemUsers;

namespace AAS.BackOffice.Infrastructure.ReactApp;

public class ReactApp
{
    public String Name { get; }
    public String ContainerId { get; }

    public BackOfficeSystemUser? SystemUser { get; set; }
    public BrowserType BrowserType { get; }
    public SidebarItem[] SidebarLinksTree { get; private set; }
    public BaseConfig BaseConfig { get; }

    public ReactApp(String name, BrowserType browserType, String containerId = "app")
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