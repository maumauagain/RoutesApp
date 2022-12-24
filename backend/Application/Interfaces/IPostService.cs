using Application.DTO;
using Application.DTO.Post;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostService
    {
        List<PostListAllDTO> GetAll();
        bool Post(PostCreateDTO post);
        PostCreateDTO GetById(int id, int userId);
        List<PostPublishedDTO> GetPublished();
        bool Put(Post post);
        bool Delete(int id);
        void LikeDeslike(int postId, bool isLike, int userId);
        string UploadImage(string base64Image);
    }
}
