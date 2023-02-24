using AAS.Domain.AccessPolicies.Attributes;
using AAS.Domain.AccessPolicies.Utils;
using System.ComponentModel.DataAnnotations;

namespace AAS.Domain.AccessPolicies;

//LastNumber = 7

public enum AccessPolicy
{
    #region Users

    [AccessPolicyBlock(AccessPolicyBlock.Users)]
    [Display(Name = "Профиль пользователя")]
    UserProfile = 1,

    [AccessPolicyBlock(AccessPolicyBlock.Users)]
    [Display(Name = "Просмотр пользователей")]
    UsersRead = 2,

    [AccessPolicyBlock(AccessPolicyBlock.Users)]
    [Display(Name = "Редактирование пользователей")]
    UsersUpdate = 3,

    #region UserRoles

    [AccessPolicyBlock(AccessPolicyBlock.Users)]
    [Display(Name = "Просмотр ролей пользователей")]
    UserRolesRead = 6,

    [AccessPolicyBlock(AccessPolicyBlock.Users)]
    [Display(Name = "Редактирование ролей пользователей")]
    UserRolesUpdate = 7,

    #endregion

    #endregion

    #region Bids
    
    [AccessPolicyBlock(AccessPolicyBlock.Bids)]
    [Display(Name = "Просмотр заявок")]
    BidsRead = 4,

    [AccessPolicyBlock(AccessPolicyBlock.Bids)]
    [Display(Name = "Редактирование заявок")]
    BidsUpdate = 5,

    #endregion
}
