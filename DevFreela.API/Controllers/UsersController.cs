using DevFreela.Application.Commands;
using DevFreela.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }       

        //GET api/users/23
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new UserGetByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }           

            return Ok(result.Data);
        }

        //POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(UserInsertCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, null);            
        }

        //POST api/users/23/skills
        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, UserInsertSkillCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }

        //PUT api/users/23/profile-picture
        [HttpPut("{id}/profile-picture")]
        public async Task<IActionResult> PostProfilePicture(int id, IFormFile file)
        {
            var result = await _mediator.Send(new UserProfilePictureCommand(id, file));

            return Ok(result.Data);
        }
    }
}
