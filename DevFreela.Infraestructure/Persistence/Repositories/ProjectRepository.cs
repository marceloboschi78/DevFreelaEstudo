using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infraestructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {

        private readonly DevFreelaDbContext _context;
        public ProjectRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Project project)
        { 
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task AddComment(ProjectComment comment)
        {           
            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();            
        }

        public async Task<bool> Exists(int id)
        {
            var result = await _context.Projects.AnyAsync(p => p.Id == id);
            return result;
        }

        public async Task<List<Project>> GetAll(int page, int size, string search)
        {
            var projects = await _context.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))// exemplo de filtro
               .Skip(page * size)
               .Take(size)// paginacao
               .ToListAsync();

            return projects;
        }       

        public async Task<Project> GetById(int id)
        {
            var project = await _context.Projects
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task<Project> GetDetailsById(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .Where(p => !p.IsDeleted)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
