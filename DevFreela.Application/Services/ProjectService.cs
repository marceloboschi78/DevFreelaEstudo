using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _context;

        public ProjectService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search, int page = 0, int size = 3)
        {
            var projects = _context.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))// exemplo de filtro
               .Skip(page * size)
               .Take(size)// paginacao
               .ToList();

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);

        }
        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .Where(p => !p.IsDeleted)
                .SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Error($"Não encontrado projeto com id = {id}.");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }


        public ResultViewModel<int> Insert(ProjectCreateInputModel model)
        {
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            int idProject = project.Id;

            return ResultViewModel<int>.Success(idProject);
        }
        public ResultViewModel Update(int id, ProjectUpdateInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {id}.");
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
        public ResultViewModel Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {id}.");
            }

            project.SetAsDeleted();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
        public ResultViewModel Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {id}.");
            }

            project.Start();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
        public ResultViewModel Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {id}.");
            }

            project.Complete();

            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
        public ResultViewModel InsertComment(int id, ProjectCommentCreateInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {id}.");
            }

            var comment = new ProjectComment(model.Content, id, model.IdUser);

            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
