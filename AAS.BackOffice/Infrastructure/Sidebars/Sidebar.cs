using AAS.Domain.AccessPolicies;
using AAS.BackOffice.Areas.Infrastructure.Models;
using AAS.Domain.Users.SystemUsers;
using static AAS.BackOffice.Infrastructure.Sidebars.SidebarItem;

namespace AAS.BackOffice.Infrastructure.Sidebars;

public static class Sidebar
{
    private static readonly SidebarItem[] LinkItems =
    {
        ListItem(text: "Пользователи", url: "/users", iconType: SideBarIconType.Users, policy: AccessPolicy.UsersRead),
    };

    public static SidebarItem[] GetLinksTree(SystemUser? systemUser)
    {
        if (systemUser is null) return Array.Empty<SidebarItem>();

        if (systemUser.Access.AccessPolicies.Length == 0) return Array.Empty<SidebarItem>();

        return LinkItems
            .Select(li => li.Clone())
            .Where(li => li.UserHasPermission(systemUser.Access))
            .Select(li => FilterAvailableItems(li, systemUser))
            .ToArray();
    }

    private static SidebarItem FilterAvailableItems(SidebarItem item, SystemUser systemUser)
    {
        for (int i = 0; i < item.InnerItems.Length; i++)
            item.InnerItems[i] = FilterAvailableItems(item.InnerItems[i], systemUser);

        SidebarItem[] innerItems = item.InnerItems.Where(li => li.UserHasPermission(systemUser.Access)).ToArray();

        return innerItems.Any()
            ? ListGroup(item.Text, item.IconType, innerItems)
            : ListItem(item.Text, item.Url, item.IconType, item.AvailableForAccessPolicies);
    }
}
