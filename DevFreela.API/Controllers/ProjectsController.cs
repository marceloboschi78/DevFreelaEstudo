using DevFreela.API.Models;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _totalCostConfig;
        private readonly IConfigService _configService;
        public ProjectsController(IOptions<FreelanceTotalCostConfig> options, IConfigService configService)
        {
            _totalCostConfig = options.Value;
            _configService = configService;
        }
        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult GetAll(string search = "")
        {
            return Ok(_configService.GetValue());
        }

        //GET api/projects/23
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new Exception();
            return Ok();
        }

        //POST api/projects
        [HttpPost]
        public IActionResult Post(ProjectCreateInputModel model)
        {
            if (model.TotalCost < _totalCostConfig.Minimum ||
                model.TotalCost > _totalCostConfig.Maximum)
            {
                return BadRequest($"Valor do projeto fora do intervalo permitido." +
                    $" Deve estar entre {_totalCostConfig.Minimum} e {_totalCostConfig.Maximum}.");
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
