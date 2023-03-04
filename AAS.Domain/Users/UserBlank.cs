using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;
using System.Text.Json.Serialization;

namespace AAS.Domain.Users;
public class UserBlank
{
    public ID? Id { get; set; }
    public String? FirstName { get; set; }
    public String? MiddleName { get; set; }
    public String? LastName { get; set; }
    public String? Email { get; set; }
    public String? PhoneNumber { get; set; }
    public ID? RoleId { get; set; }
    public String? Password { get; set; }
    public String? RePassword { get; set; }

    [JsonIgnore]
    public String? Passwordhash => HashManager.DefinePasswordHash(Password);

    public UserBlank(ID? id, String? firstName, String? middleName, String? lastName, String? email, String? phoneNumber, ID? roleId, String? password, String? rePassword)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        RoleId = roleId;
        Password = password;
        RePassword = rePassword;
    }
}
