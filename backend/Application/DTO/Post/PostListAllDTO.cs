using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Post
{
    public class PostListAllDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
        public string State { get; set; }
        public string CreatedOn { get; set; }
        public string Description { get; set; }
    }
}
