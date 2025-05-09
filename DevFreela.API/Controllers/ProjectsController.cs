using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        private readonly DevFreelaDbContext _context;
        public ProjectsController(DevFreelaDbContext context)
        {
            _context = context;
        }

        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult GetAll(string search = "", int page = 0, int size = 3)
        {
            //return Ok(_configService.GetValue());
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))// exemplo de filtro
                .Skip(page * size)
                .Take(size)// paginacao
                .ToList();

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return Ok(model);
        }

        //GET api/projects/23
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //throw new Exception();
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .Where(p => !p.IsDeleted)
                .SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound($"Não encontrado projeto com id = {id}.");
            }

            var model = ProjectViewModel.FromEntity(project);

            return Ok(model);
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
            
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            int idProject = project.Id;
            
            return CreatedAtAction(nameof(GetById), new { id = idProject }, model);
        }

        //PUT api/projects/23
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProjectUpdateInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound($"Não encontrado projeto com id = {id}.");
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //DELETE api/projects/23
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound($"Não encontrado projeto com id = {id}.");
            }

            project.SetAsDeleted();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //PUT api/projects/23/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound($"Não encontrado projeto com id = {id}.");
            }

            project.Start();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //PUT api/projects/23/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound($"Não encontrado projeto com id = {id}.");
            }

            project.Complete();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //POST api/projects/23/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, ProjectCreateCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound($"Não encontrado projeto com id = {id}.");
            }

            var comment = new ProjectComment(model.Content, id, model.IdUser);

            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = id}, model.Content);
        }
    }
}
