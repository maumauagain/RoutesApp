using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Post
{
    public class PostCreateDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string FromLat { get; set; }
        public string FromAlt { get; set; }
        public string ToLat { get; set; }
        public string ToAlt { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string ToStreet { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public bool HasRamp { get; set; }
        public bool HasLevel { get; set; }
        public bool HasObstacle { get; set; }
        public bool UserHasLiked { get; set; }
        public bool UserHasDisliked { get; set; }
        public bool IsPublished { get; set; }
        public bool IsRejected { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public List<PostImageCreateDTO> Images { get; set; }

    }
}
