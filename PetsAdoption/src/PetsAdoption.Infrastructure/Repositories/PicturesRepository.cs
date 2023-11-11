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
public class PicturesRepository : Repository<Picture>, IPicturesRepository
{
    public PicturesRepository(PetsAdoptionDBContext dbContext, IOptions<FeatureFlags> featureFlagsOption) : base(dbContext, featureFlagsOption)
    {
    }

    public async Task<Picture?> Get(Guid petId, Guid pictureId)
    {
        return await DbSet.SingleOrDefaultAsync(picture => picture.Id == pictureId && picture.Pet.Id == petId);
    }

}
