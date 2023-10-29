using Microsoft.EntityFrameworkCore;
using PetsAdoption.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Contexts;
public class PetsAdoptionDBContext : DbContext
{
    public PetsAdoptionDBContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Pet>? Pets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetsAdoptionDBContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
