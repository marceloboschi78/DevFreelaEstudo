using DevFreela.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //GET api/users/23
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        //POST api/users
        [HttpPost]
        public IActionResult Post(UserCreateInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new {id = 1}, null);
        }

        //POST api/users/23/skills
        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillCreateInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, null);
        }

        //PUT api/users/23/profile-picture
        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(IFormFile file)
        {
            var description = $"File: {file.FileName} - Size: {file.Length} bytes";

            //processar imagem

            return Ok(description);
        }
    }
}
