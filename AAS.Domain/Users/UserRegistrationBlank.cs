using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;

namespace AAS.Domain.Users;

public class UserRegistrationBlank
{
    public ID? Id { get; set; }
    public String? FirstName { get; set; }
    public String? MiddleName { get; set; }
    public String? LastName { get; set; }
    public String? Email { get; set; }
    public String? PhoneNumber { get; set; }
    public String? Password { get; set; }
    public String? RePassword { get; set; }

    public String? PasswordHash => HashManager.DefinePasswordHash(Password);
}