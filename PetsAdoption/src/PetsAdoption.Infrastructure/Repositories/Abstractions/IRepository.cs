using PetsAdoption.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Repositories.Abstractions;

public interface IRepository<T> where T : ModelBase
{
    Task<T?> GetById(Guid id);
    Task Add(T model);
    Task<T> Update(T model);
    Task<List<T>> GetAll();
    Task Delete(T model);
}
