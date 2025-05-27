using DevFreela.Application.CQRS.Commands;
using DevFreela.Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {           
        private readonly IMediator _mediator;      

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET api/skills
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new SkillGetAllQuery());
            
            return Ok(result.Data);
        }

        //POST api/skills
        [HttpPost]
        public async Task<IActionResult> Post(SkillInsertCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAll),null, result.Data);
        }
    }
}
