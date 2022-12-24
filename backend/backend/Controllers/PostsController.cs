using Application.DTO;
using Application.DTO.Post;
using Application.Interfaces;
using Auth.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var posts = _postService.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, int userId)
        {
            return Ok(_postService.GetById(id, userId));
        }

        [HttpGet("/api/Posts/getPublished")]
        public IActionResult GetPublished(int id, int userId)
        {
            return Ok(_postService.GetPublished());
        }

        [HttpPost]
        public IActionResult Post(PostCreateDTO post)
        {
            if (post.Images is null || post.Images.Count == 0)
                return BadRequest();

            var postImages = new List<PostImageCreateDTO>();

            try
            {
                foreach(var image in post.Images)
                {
                    var newImage = new PostImageCreateDTO();
                    newImage.FileUrl = _postService.UploadImage(image.FileUrl);
                    postImages.Add(newImage);
                }               
            }catch(Exception)
            {
                return BadRequest("Error in Image Upload, please try again");
            }

            string _userId = new TokenService().GetValueFromClaim(HttpContext.User.Identity, ClaimTypes.NameIdentifier);

            post.UserId = int.Parse(_userId);
            post.Images = postImages;

            return Ok(_postService.Post(post));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_postService.Delete(id))
                return Ok();

            return NotFound();
        }

        #region Post Likes
        [HttpPost("/api/Posts/like"), Authorize]
        public IActionResult LikePost([FromQuery] int postId, bool isLike)
        {
            string _userId = new TokenService().GetValueFromClaim(HttpContext.User.Identity, ClaimTypes.NameIdentifier);
            _postService.LikeDeslike(postId, isLike, int.Parse(_userId));

            return Ok();
        }

        #endregion

    }
}
