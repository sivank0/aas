using AAS.Domain.AccessPolicies;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Types.IDs;

namespace AAS.BackOffice.Areas.Users.Models;

public class BackOfficeSystemUser
{
    public ID Id { get; }
    public String Email { get; }
    public String FullName { get; }
    public Boolean HasFullAccess { get; }
    public AccessPolicy[] AvailableAccessPolicies { get; }

    public BackOfficeSystemUser(SystemUser systemUser)
    {
        Id = systemUser.Id;
        Email = systemUser.Email;
        FullName = systemUser.FullName;
        HasFullAccess = systemUser.Access.HasFullAccess;
        AvailableAccessPolicies = systemUser.Access.AccessPolicies;
    }
}
