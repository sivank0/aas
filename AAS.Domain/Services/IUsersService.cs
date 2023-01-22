using AAS.Domain.Users;
using PMS.Tools.Types.IDs;

namespace AAS.Domain.Services;
public interface IUsersService
{
    String SaveUser(UserBlank userBlank);
    User? GetUser(ID id);
}
