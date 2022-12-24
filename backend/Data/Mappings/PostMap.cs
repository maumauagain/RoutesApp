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
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.HasOne(p => p.CreatedBy).WithMany().HasForeignKey(p => p.UserId);
            builder.HasMany(p => p.Images).WithOne(i => i.Post);
            builder.HasMany(p => p.Likes).WithOne(i => i.Post);
            builder.HasMany(p => p.Dislikes).WithOne(i => i.Post);
            builder.Property(p => p.FromAlt).IsRequired();
            builder.Property(p => p.FromLat).IsRequired();
            builder.Property(p => p.ToAlt).IsRequired();
            builder.Property(p => p.ToLat).IsRequired();
            builder.Property(p => p.Street).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ToStreet).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Neighborhood).IsRequired().HasMaxLength(50);
            builder.Property(p => p.City).IsRequired().HasMaxLength(50);
            builder.Property(p => p.State).IsRequired().HasMaxLength(2);
            builder.Property(p => p.Description).HasMaxLength(100);
            builder.Property(p => p.HasLevel).HasDefaultValue(0);
            builder.Property(p => p.HasObstacle).HasDefaultValue(0);
            builder.Property(p => p.HasRamp).HasDefaultValue(0);
            builder.Property(p => p.IsPublished).HasDefaultValue(0);
            builder.Property(p => p.IsRejected).HasDefaultValue(0);
            builder.Property(p => p.IsActive).HasDefaultValue(1);
            builder.Property(p => p.CreatedOn).HasDefaultValue(DateTime.Now);

        }
    }
}
