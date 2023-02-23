namespace AAS.Domain.AccessPolicies;

public enum AccessPolicy
{
    #region Users

    UserProfile = 1,
    UsersRead = 2,
    UsersUpdate = 3,

    #endregion

    #region Bids

    BidsRead = 4,
    BidsUpdate = 5,

    #endregion
}
