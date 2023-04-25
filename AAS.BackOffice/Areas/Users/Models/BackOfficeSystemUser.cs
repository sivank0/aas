#region

using AAS.Domain.AccessPolicies;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.BackOffice.Areas.Users.Models;

public class BackOfficeSystemUser
{
    public ID Id { get; }
    public string Email { get; }
    public string FullName { get; }
    public bool HasFullAccess { get; }
    public AccessPolicy[] AvailableAccessPolicies { get; }

    public BackOfficeSystemUser(SystemUser systemUser)
    {
        Id = systemUser.Id;
        Email = systemUser.Email;
        FullName = systemUser.FullName;
        AvailableAccessPolicies = systemUser.Access.AccessPolicies;
    }
}