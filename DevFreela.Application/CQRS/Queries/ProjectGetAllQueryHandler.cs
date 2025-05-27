using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Queries
{
    public class ProjectGetAllQueryHandler : IRequestHandler<ProjectGetAllQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly DevFreelaDbContext _context;

        public ProjectGetAllQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }        
public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(ProjectGetAllQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .Where(p => !p.IsDeleted && (request.Search == "" || p.Title.Contains(request.Search) || p.Description.Contains(request.Search)))// exemplo de filtro
               .Skip(request.Page * request.Size)
               .Take(request.Size)// paginacao
               .ToListAsync();

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
