﻿using Authentication.Model.Entities;
using Infrastructure.DataAccess.Interfaces;

namespace Authentication.DataAccess.Interfaces
{
    public interface IPasswordHistoryRepository : IBaseRepository<PasswordHistory, long>
    {

    }
}
