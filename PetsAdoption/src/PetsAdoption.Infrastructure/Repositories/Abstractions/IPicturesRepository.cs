using PetsAdoption.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Repositories.Abstractions;
public interface IPicturesRepository : IRepository<Picture>
{
    Task<Picture?> Get(Guid petId, Guid pictureId);
}
