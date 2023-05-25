#region

using AAS.Domain.Files;
using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Users;

public class UserRegistrationBlank
{
    public ID? Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    
    public FileBlank? FileBlank { get; set; }

    public string? Password { get; set; }
    public string? RePassword { get; set; }

    public string? PasswordHash => HashManager.DefinePasswordHash(Password);
}