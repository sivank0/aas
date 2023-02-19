using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;
using System.Text.Json.Serialization;

namespace AAS.Domain.Users;
public class UserBlank
{
    public ID? Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? RePassword { get; set; }

    [JsonIgnore]
    public string? Passwordhash => HashManager.DefinePasswordHash(Password);

    public UserBlank(ID? id, string? firstName, string? middleName, string? lastName, string? email, string? phoneNumber, string? password, string? rePassword)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        RePassword = rePassword;
    }
}
