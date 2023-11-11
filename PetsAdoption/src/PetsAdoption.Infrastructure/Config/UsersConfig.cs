using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetsAdoption.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Infrastructure.Config;
public class UsersConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (Roles)Enum.Parse(typeof(Roles), v))
            .HasMaxLength(40);

        builder.Property(u => u.UserName)
            .HasMaxLength(50);

        builder.Property(u => u.Password)
            .HasMaxLength(500);
    }
}
