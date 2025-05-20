using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {       
        private readonly IUserService _userService;       

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //GET api/users/23
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);                

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }           

            return Ok(result.Data);
        }

        //POST api/users
        [HttpPost]
        public IActionResult Post(UserCreateInputModel model)
        {
            var result = _userService.Insert(model);            

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);            
        }

        //POST api/users/23/skills
        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillCreateInputModel model)
        {
            var result = _userService.InsertSkill(id, model);           

            return CreatedAtAction(nameof(GetById), new { id = id }, result.Data);
        }

        //PUT api/users/23/profile-picture
        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var result = _userService.ProfilePicture(id, file);

            return Ok(result.Data);
        }
    }
}
