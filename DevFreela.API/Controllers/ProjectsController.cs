using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        //private readonly FreelanceTotalCostConfig _totalCostConfig;
        //private readonly IConfigService _configService;        
        //public ProjectsController(IOptions<FreelanceTotalCostConfig> options, IConfigService configService)
        //{
        //    _totalCostConfig = options.Value;
        //    _configService = configService;
        //}
        
        private readonly IProjectService _projectService;
    
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult GetAll(string search = "", int page = 0, int size = 15)
        {
            //return Ok(_configService.GetValue());
            var result = _projectService.GetAll(search, page, size);

            return Ok(result.Data);
        }

        //GET api/projects/23
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //throw new Exception();
            
            var result = _projectService.GetById(id);
            
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }            
            
            return Ok(result.Data);
        }

        //POST api/projects
        [HttpPost]
        public IActionResult Post(ProjectCreateInputModel model)
        {
            //if (model.TotalCost < _totalCostConfig.Minimum ||
            //    model.TotalCost > _totalCostConfig.Maximum)
            //{
            //    return BadRequest($"Valor do projeto fora do intervalo permitido." +
            //        $" Deve estar entre {_totalCostConfig.Minimum} e {_totalCostConfig.Maximum}.");
            //}
            
            var result = _projectService.Insert(model);
                        
            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        //PUT api/projects/23
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProjectUpdateInputModel model)
        {
            var result = _projectService.Update(id, model);
            
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }

        //DELETE api/projects/23
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _projectService.Delete(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }           

            return NoContent();
        }

        //PUT api/projects/23/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _projectService.Start(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }            

            return NoContent();
        }

        //PUT api/projects/23/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _projectService.Complete(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }

        //POST api/projects/23/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, ProjectCommentCreateInputModel model)
        {
            var result = _projectService.InsertComment(id, model);

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = id}, model.Content);
        }
    }
}
