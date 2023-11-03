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
public class ContactsRepository : Repository<Contact>, IContactsRepository
{
    public ContactsRepository(PetsAdoptionDBContext dbContext, IOptions<FeatureFlags> featureFlagsOption) 
        : base(dbContext, featureFlagsOption)
    {
    }
}
