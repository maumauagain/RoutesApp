using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Cpf).HasMaxLength(14);
            builder.Property(p => p.Password).HasMaxLength(50).IsRequired();
            builder.Property(p => p.IsActive).HasDefaultValue(1);
            builder.Property(p => p.CreatedOn).HasDefaultValue(DateTime.Now);
            builder.Property(p => p.City).HasMaxLength(50);
            builder.Property(p => p.State).HasMaxLength(2);

        }
    }
}
