﻿using AAS.Tools.Types.IDs;

namespace AAS.Domain.Users;
public class UserBlank
{
    public ID? Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
