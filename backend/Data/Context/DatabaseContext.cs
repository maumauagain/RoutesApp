using Data.Extensions;
using Data.Mappings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImages> PostImages { get; set; }
        public DbSet<PostLikes> PostLikes { get; set; }
        public DbSet<PostDislikes> PostDislikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new PostImagesMap());
            modelBuilder.ApplyConfiguration(new PostLikesMap());
            modelBuilder.ApplyConfiguration(new PostDislikesMap());

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }

    }
}
