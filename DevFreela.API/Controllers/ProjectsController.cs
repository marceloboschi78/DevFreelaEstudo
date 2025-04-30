using DevFreela.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _TotalCostConfig;
        public ProjectsController(IOptions<FreelanceTotalCostConfig> options)
        {
            _TotalCostConfig = options.Value;
        }
        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult GetAll(string search = "")
        {
            return Ok();
        }

        //GET api/projects/23
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        //POST api/projects
        [HttpPost]
        public IActionResult Post(ProjectCreateInputModel model)
        {
            if (model.TotalCost < _TotalCostConfig.Minimum ||
                model.TotalCost > _TotalCostConfig.Maximum)
            {
                return BadRequest($"Valor do projeto fora do intervalo permitido." +
                    $" Deve estar entre {_TotalCostConfig.Minimum} e {_TotalCostConfig.Maximum}.");
            }
            return CreatedAtAction(nameof(GetById), new { id = 1 }, null);
        }

        //PUT api/projects/23
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProjectUpdateInputModel model)
        {
            return NoContent();
        }

        //DELETE api/projects/23
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        //PUT api/projects/23/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        //PUT api/projects/23/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        //POST api/projects/23/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, ProjectCreateCommentInputModel model)
        {
            return CreatedAtAction(nameof(PostComment), model.IdProject, model.Content);
        }
    }
}
