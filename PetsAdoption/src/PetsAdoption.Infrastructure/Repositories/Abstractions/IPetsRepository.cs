﻿using PetsAdoption.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Repositories.Abstractions;
public interface IPetsRepository : IRepository<Pet>
{
    Task<List<Pet>> GetByContact(Guid contactId);
}
