﻿using AAS.Domain.Users;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Repositories;

public interface IUsersRepository
{
    public void SaveUser(UserBlank userBlank);
    public User? GetUser(ID id);
    public User? GetUser(String userName);
    public void RemoveUser(ID userId);
}