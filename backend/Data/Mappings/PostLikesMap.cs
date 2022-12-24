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
    public class PostLikesMap : IEntityTypeConfiguration<PostLikes>
    {
        public void Configure(EntityTypeBuilder<PostLikes> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.HasOne(p => p.Post).WithMany(po => po.Likes).HasForeignKey(p => p.PostId);
            builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
        }
    }
}
