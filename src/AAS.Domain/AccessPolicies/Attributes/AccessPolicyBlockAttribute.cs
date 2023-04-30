#region

using AAS.Domain.AccessPolicies.Utils;
using AAS.Tools.Extensions;

#endregion

namespace AAS.Domain.AccessPolicies.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class AccessPolicyBlockAttribute : Attribute
{
    public int Key { get; }
    public string DisplayName { get; }

    public AccessPolicyBlockAttribute(AccessPolicyBlock accessPolicyBlock)
    {
        Key = (int)accessPolicyBlock;
        DisplayName = accessPolicyBlock.GetDisplayName();
    }
}