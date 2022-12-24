using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Auth.Services;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Get()
        {

            var users = _userService.Get();
            return Ok(users);
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Post(UserDTO user)
        {
            if (this._userService.Post(user))
                return Ok();
            else
                return BadRequest("Something went wrong. Please try again");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPut]
        public IActionResult Put(UserUpdateDTO user)
        {
            return Ok(_userService.Put(user));
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            string _userId = new TokenService().GetValueFromClaim(HttpContext.User.Identity, ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(_userId))
                return Unauthorized();

            int id = int.Parse(_userId);
            return Ok(_userService.Delete(id));
        }

        [HttpPost("Auth"), AllowAnonymous]
        public IActionResult Authenticate(UserAuthenticateRequestDTO user)
        {
            return Ok(this._userService.Authenticate(user));
        }
    }
}
