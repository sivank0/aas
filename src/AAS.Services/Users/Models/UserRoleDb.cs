#region

using AAS.Domain.AccessPolicies;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Services.Users.Models;

public class UserRoleDb
{
    public ID Id { get; set; }
    public string Name { get; set; }
    public AccessPolicy[] AccessPolicies { get; set; }

    public ID CreatedUserId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public ID ModifiedUserId { get; set; }
    public DateTime ModifiedDateTimeUtc { get; set; }

    public bool IsRemoved { get; set; }

    public UserRoleDb(ID id, string name, AccessPolicy[] accessPolicies, ID createdUserId, DateTime createdDateTimeUtc,
        ID modifiedUserId, DateTime modifiedDateTimeUtc, bool isRemoved)
    {
        Id = id;
        Name = name;
        AccessPolicies = accessPolicies;
        CreatedUserId = createdUserId;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedUserId = modifiedUserId;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}