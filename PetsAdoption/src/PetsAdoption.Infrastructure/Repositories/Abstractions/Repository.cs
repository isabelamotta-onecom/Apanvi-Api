using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetsAdoption.Domain.Models;
using PetsAdoption.Infrastructure.Contexts;
using PetsAdoption.Infrastructure.Settings;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Repositories.Abstractions;
public abstract class Repository<T> : IRepository<T> where T : ModelBase
{
    private readonly ConcurrentDictionary<Guid, T> _db = new ConcurrentDictionary<Guid, T>();
    private readonly IOptions<FeatureFlags> _featureFlagsOption;
    private readonly PetsAdoptionDBContext _petsAdoptionDBContext;
    protected readonly DbSet<T> DbSet;

    public Repository(PetsAdoptionDBContext dbContext, IOptions<FeatureFlags> featureFlagsOption)
    {
        _featureFlagsOption = featureFlagsOption;
        _petsAdoptionDBContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    public virtual async Task<List<T>> GetAll()
    {
        return await _petsAdoptionDBContext.Set<T>().ToListAsync();
    }

    public async Task<T> Update(T model)
    {
        DbSet.Update(model);
        await _petsAdoptionDBContext.SaveChangesAsync();
        return model;
    }
    public async Task Add(T model)
    {
        if (_featureFlagsOption.Value.UseSQLServer == false)
        {
            await AddToDictionary(model);
        }

        await DbSet.AddAsync(model);
        await _petsAdoptionDBContext.SaveChangesAsync();
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        if (_featureFlagsOption.Value.UseSQLServer == false)
        {
            return await GetFromDictionary(id);
        }

        return await DbSet.FindAsync(id);
    }

    public async Task Delete(T model)
    {
        DbSet.Remove(model);
        await _petsAdoptionDBContext.SaveChangesAsync();
    }

    private async Task AddToDictionary(T model)
    {
        _db.TryAdd(model.Id, model);
        await Task.CompletedTask;
    }

    private async Task<T?> GetFromDictionary(Guid id)
    {
        if (_db.TryGetValue(id, out var model))
        {
            return model;
        }
        return await Task.FromResult(default(T?));
    }
}
