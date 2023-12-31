﻿using PetsAdoption.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Repositories.Abstractions;
public interface IUsersRepository : IRepository<User>
{
   Task<User?> GetByUserName(string userName);
}
