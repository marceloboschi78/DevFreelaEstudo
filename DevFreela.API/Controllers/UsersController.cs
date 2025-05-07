using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public UsersController(DevFreelaDbContext context)
        {
            _context = context;
        }

        //GET api/users/23
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.UserSkills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefault(u => u.Id == id);                

            if (user == null)
            {
                return NotFound($"Não foi encontrado usuário com id = {id}.");
            }

            var model = UserViewModel.FromEntity(user);

            return Ok(model);
        }

        //POST api/users
        [HttpPost]
        public IActionResult Post(UserCreateInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();

            var IdUser = user.Id;            

            return CreatedAtAction(nameof(GetById), new { id = IdUser }, model);            
        }

        //POST api/users/23/skills
        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillCreateInputModel model)
        {
            var userSkills = model.SkillIds.Select(skillId => new UserSkill(id, skillId)).ToList();

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = id }, model);
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
