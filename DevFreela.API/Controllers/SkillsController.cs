using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public SkillsController(DevFreelaDbContext context)
        {
            _context = context;
        }

        //GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _context.Skills
                .Where(s => !s.IsDeleted)
                .ToList();
            
            var model = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();

            return Ok(model);
        }

        //POST api/skills
        [HttpPost]
        public IActionResult Post(SkillCreateInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAll),null, model);
        }
    }
}
