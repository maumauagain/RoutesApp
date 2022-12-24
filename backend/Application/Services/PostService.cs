using Application.DTO;
using Application.DTO.Post;
using Application.Interfaces;
using AutoMapper;
using Azure.Storage.Blobs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public bool Delete(int id)
        {
            var post = _postRepository.Find(p => p.Id == id);
            if(post != null)
                return _postRepository.Delete(id);

            return false;
        }

        public List<PostListAllDTO> GetAll()
        {
            var posts = _postRepository.GetAll();
            var postsDTO = _mapper.Map<List<PostListAllDTO>>(posts);

            return postsDTO.OrderByDescending(p => p.Id).ToList();
        }

        public PostCreateDTO GetById(int id, int userId )
        {
            var post = _postRepository.FindById(id);
            var user = _userRepository.Find(u => u.Id == post.UserId);
            var postCreateDTO = _mapper.Map<PostCreateDTO>(post);
            postCreateDTO.UserHasLiked = post.Likes.Select(l => l.UserId == userId).Count() > 0;
            postCreateDTO.UserHasDisliked = post.Dislikes.Select(l => l.UserId == userId).Count() > 0;
            postCreateDTO.LikesCount = post.Likes.Count();
            postCreateDTO.DislikesCount = post.Dislikes.Count();
            postCreateDTO.UserEmail = user.Email;

            return postCreateDTO;
        }

        public List<PostPublishedDTO> GetPublished()
        {
            var post = _postRepository.Query(p => p.IsActive && p.IsPublished).ToList();
            return _mapper.Map<List<PostPublishedDTO>>(post);
        }

        public void LikeDeslike(int postId, bool isLike, int userId)
        {
            _postRepository.Like(postId, isLike, userId);
        }

        public bool Post(PostCreateDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            _postRepository.Create(post);
            return true;
        }

        public bool Put(Post post)
        {
            throw new NotImplementedException();
        }

        public string UploadImage(string base64Image)
        {
            var filename = Guid.NewGuid().ToString() + ".jpg";

            var imageFormatted = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

            byte[] image = Convert.FromBase64String(imageFormatted);

            var blobClient = new BlobClient(_configuration.GetSection("Azure").GetValue<string>("BlobConnectionString"), _configuration.GetSection("Azure").GetValue<string>("BlobContainer"), filename);

            using (var stream = new MemoryStream(image))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
