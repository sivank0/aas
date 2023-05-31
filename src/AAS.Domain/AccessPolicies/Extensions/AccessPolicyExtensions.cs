#region

using AAS.Domain.AccessPolicies.Utils;
using AAS.Domain.Users.UserAccesses;

#endregion

namespace AAS.Domain.AccessPolicies.Extensions;

public static class AccessPolicyExtensions
{
    public static int Key(this AccessPolicy accessPolicy)
    {
        return (int)accessPolicy;
    }

    public static int[] Keys(this AccessPolicy[] accessPolicies)
    {
        return accessPolicies.Select(Key).ToArray();
    }

    public static AccessPolicyDetails ToDetails(this AccessPolicy accessPolicy)
    {
        return new AccessPolicyDetails(accessPolicy);
    }

    public static AccessPolicyDetails[] ToDetails(this AccessPolicy[] accessPolicies)
    {
        return accessPolicies.Select(ToDetails).ToArray();
    }

    public static Boolean UserHasPermission(this AccessPolicy accessPolicy, UserAccess? userAccess)
    {
        if (userAccess is null) return false;

        return userAccess.AccessPolicies.Any(ap => ap.Key() == accessPolicy.Key());
    }
}