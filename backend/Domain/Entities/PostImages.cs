using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostImages
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public string FileUrl { get; set; }
    }
}
