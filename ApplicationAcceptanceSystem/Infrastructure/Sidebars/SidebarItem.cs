using AAS.Domain.AccessPolicies;
using Microsoft.AspNetCore.Mvc;
using AAS.BackOffice.Areas.Infrastructure.Models;
using AAS.Domain.Users.UserAccesses;
using AAS.Tools.Types.IDs;

namespace AAS.BackOffice.Infrastructure.Sidebars;

public class SidebarItem
{
    private static readonly Dictionary<string, Type> ControllerTypes = typeof(SidebarItem).Assembly.GetTypes()
        .Where(t => typeof(Controller).IsAssignableFrom(t) && !t.IsAbstract).ToDictionary(t => t.Name);

    public ID Id { get; }
    public string Text { get; }
    public string Url { get; }
    public SideBarIconType IconType { get; }
    public SidebarItem[] InnerItems { get; }
    public AccessPolicy[] AvailableForAccessPolicies { get; }

    public static SidebarItem ListItem(string text, string url, SideBarIconType iconType, AccessPolicy policy)
        => new(text, url, iconType, new[] { policy });

    public static SidebarItem ListItem(string text, string url, SideBarIconType iconType, AccessPolicy[] policies)
        => new(text, url, iconType, policies);

    private SidebarItem(string text, string url, SideBarIconType iconType, AccessPolicy[] availableForAccessPolicies)
    {
        Id = ID.New();
        Text = text;
        Url = url;
        IconType = iconType;
        InnerItems = new SidebarItem[0];
        AvailableForAccessPolicies = availableForAccessPolicies;
    }

    public static SidebarItem ListGroup(string text, SideBarIconType iconType, SidebarItem[] innerItems)
        => new(text, iconType, innerItems);

    private SidebarItem(string text, SideBarIconType iconType, SidebarItem[] innerItems)
    {
        Id = ID.New();
        Text = text;
        Url = string.Empty;
        IconType = iconType;
        InnerItems = innerItems;
        AvailableForAccessPolicies = innerItems.SelectMany(i => i.AvailableForAccessPolicies).Distinct().ToArray();
    }

    public bool UserHasPermission(UserAccess? access)
    {
        if (access is null) return false;

        //return AvailableForAccessPolicies.Any(p => p.UserHasPermission(access));
        return true;
    }

    public SidebarItem Clone()
    {
        return InnerItems.Any()
            ? ListGroup(Text, IconType, InnerItems.Select(i => i.Clone()).ToArray())
            : ListItem(Text, Url, IconType, AvailableForAccessPolicies);
    }
}
