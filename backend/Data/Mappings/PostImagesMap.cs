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
    public class PostImagesMap : IEntityTypeConfiguration<PostImages>
    {
        public void Configure(EntityTypeBuilder<PostImages> builder)
        {
            builder.Property(i => i.Id).IsRequired();
            builder.Property(i => i.FileUrl).HasMaxLength(1000);
            builder.HasOne(i => i.Post).WithMany(p => p.Images).HasForeignKey(i => i.PostId);
        }
    }
}
