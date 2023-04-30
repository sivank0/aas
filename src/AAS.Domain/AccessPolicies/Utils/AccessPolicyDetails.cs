#region

using System.Reflection;
using AAS.Domain.AccessPolicies.Attributes;
using AAS.Domain.AccessPolicies.Extensions;
using AAS.Tools.Extensions;

#endregion

namespace AAS.Domain.AccessPolicies.Utils;

public class AccessPolicyDetails
{
    public int Key { get; set; }
    public string DisplayName { get; set; }
    public int BlockKey { get; set; }
    public string BlockDisplayName { get; set; }

    public AccessPolicyDetails(AccessPolicy accessPolicy)
    {
        FieldInfo? field = accessPolicy.GetType().GetField(accessPolicy.ToString());

        if (field is null) throw new Exception("Политика доступа не найдена");

        AccessPolicyBlockAttribute? blockAttribute = field.GetCustomAttribute<AccessPolicyBlockAttribute>(false);

        if (blockAttribute is null) throw new Exception("Не удалось определить блок политики доступа");

        Key = accessPolicy.Key();
        DisplayName = accessPolicy.GetDisplayName();
        BlockKey = blockAttribute.Key;
        BlockDisplayName = blockAttribute.DisplayName;
    }
}