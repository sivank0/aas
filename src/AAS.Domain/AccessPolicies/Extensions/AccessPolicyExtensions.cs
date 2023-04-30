#region

using AAS.Domain.AccessPolicies.Utils;

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
}