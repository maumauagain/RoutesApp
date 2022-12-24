using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(
                    new User { Id = 1, Name = "Amauri Martins", Email = "amauri@amauri.com", Cpf = "", Password = "teste", CreatedOn = DateTime.Now }
                    );

            builder.Entity<Post>()
                .HasData(
                    new Post
                    {
                        Id = 1,
                        City = "Cornelio Procopio",
                        Neighborhood = "Centro",
                        Street = "Rua Piauí",
                        CreatedOn = DateTime.Now,
                        UserId = 1,
                        FromAlt = "-23.3421175",
                        ToAlt = "-23.3421175",
                        FromLat = "-51.1852585",
                        ToLat = "-51.1852585",
                        ToStreet = "Rua Piauí",
                        State = "PR"
                    }
                    );

            builder.Entity<PostImages>()
                .HasData(
                        new PostImages
                        {
                            Id = 1,
                            PostId = 1,
                            FileUrl = "http://a.tile.osm.org/16/23449/37139.png"
                        },
                            new PostImages
                            {
                                Id = 2,
                                PostId = 1,
                                FileUrl = "http://a.tile.osm.org/16/23449/37139.png"
                            },
                            new PostImages
                            {
                                Id = 3,
                                PostId = 1,
                                FileUrl = "http://a.tile.osm.org/16/23449/37139.png"
                            },
                            new PostImages
                            {
                                Id = 4,
                                PostId = 1,
                                FileUrl = "http://a.tile.osm.org/16/23449/37139.png"
                            }
                );
            return builder;
        }
    }
}
