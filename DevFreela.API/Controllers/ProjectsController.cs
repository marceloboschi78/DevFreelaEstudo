using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.CQRS.Queries;
using DevFreela.Application.CQRS.Commands;

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
        
        
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {            
            _mediator = mediator;
        }

        //GET api/projects?search=crm
        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "", int page = 0, int size = 15)
        {
            //return Ok(_configService.GetValue());
            //var result = _projectService.GetAll(search, page, size);

            var query = new ProjectGetAllQuery(search, page, size);
            var result = await _mediator.Send(query);

            return Ok(result.Data);
        }

        //GET api/projects/23
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //throw new Exception();
                        
            var result = await _mediator.Send(new ProjectGetByIdQuery(id));
            
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }            
            
            return Ok(result.Data);
        }

        //POST api/projects
        [HttpPost]
        public async Task<IActionResult> Post(ProjectInsertCommand command)
        {
            //if (model.TotalCost < _totalCostConfig.Minimum ||
            //    model.TotalCost > _totalCostConfig.Maximum)
            //{
            //    return BadRequest($"Valor do projeto fora do intervalo permitido." +
            //        $" Deve estar entre {_totalCostConfig.Minimum} e {_totalCostConfig.Maximum}.");
            //}
            
            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        //PUT api/projects/23
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProjectUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }

        //DELETE api/projects/23
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new ProjectDeleteCommand(id));

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }           

            return NoContent();
        }

        //PUT api/projects/23/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            var result = await _mediator.Send(new ProjectStartCommand(id));

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }            

            return NoContent();
        }

        //PUT api/projects/23/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _mediator.Send(new ProjectCompleteCommand(id));

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }

        //POST api/projects/23/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, ProjectInsertCommentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = id}, command.Content);
        }
    }
}
