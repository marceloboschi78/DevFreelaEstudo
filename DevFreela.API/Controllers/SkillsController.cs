using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {        
        private readonly ISkillService _skillService;        

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        //GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _skillService.GetAll();
            
            return Ok(result.Data);
        }

        //POST api/skills
        [HttpPost]
        public IActionResult Post(SkillCreateInputModel model)
        {
            var result = _skillService.Insert(model);            

            return CreatedAtAction(nameof(GetAll),null, result.Data);
        }
    }
}
