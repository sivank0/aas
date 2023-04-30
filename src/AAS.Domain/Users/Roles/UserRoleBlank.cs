#region

using AAS.Domain.AccessPolicies;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Users.Roles;

public class UserRoleBlank
{
    public ID? Id { get; set; }
    public string? Name { get; set; }
    public AccessPolicy[] AccessPolicies { get; set; }

    public UserRoleBlank(ID? id, string? name, AccessPolicy[] accessPolicies)
    {
        Id = id;
        Name = name;
        AccessPolicies = accessPolicies;
    }
}