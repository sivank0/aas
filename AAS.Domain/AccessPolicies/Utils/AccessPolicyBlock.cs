#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace AAS.Domain.AccessPolicies.Utils;

public enum AccessPolicyBlock
{
    [Display(Name = "Пользователи")] Users = 1,

    [Display(Name = "Заявки")] Bids = 2
}