using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetsAdoption.Domain.Models;
using PetsAdoption.Infrastructure.Contexts;
using PetsAdoption.Infrastructure.Repositories.Abstractions;
using PetsAdoption.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Repositories;
public class PetsRepository : Repository<Pet>, IPetsRepository
{
    public PetsRepository(PetsAdoptionDBContext dbContext, IOptions<FeatureFlags> featureFlagsOption) 
        : base(dbContext, featureFlagsOption)
    {
    }

    public override async Task<Pet?> GetById(Guid id)
    {
        return await DbSet.Include(pet => pet.Contact).SingleOrDefaultAsync(pet => pet.Id == id);
    }

    public override async Task<List<Pet>> GetAll()
    {
        return await DbSet.Include(pet => pet.Contact).ToListAsync();
    }

    public async Task<List<Pet>> GetByContact(Guid contactId)
    {
        return await DbSet.Where(pet => pet.Contact != null && pet.Contact.Id == contactId).ToListAsync();
    }
}
