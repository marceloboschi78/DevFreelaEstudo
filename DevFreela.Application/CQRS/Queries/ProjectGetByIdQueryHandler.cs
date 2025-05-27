using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Queries
{
    public class ProjectGetByIdQueryHandler : IRequestHandler<ProjectGetByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _context;

        public ProjectGetByIdQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<ProjectViewModel>> Handle(ProjectGetByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .Where(p => !p.IsDeleted)
                .SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Error($"Não encontrado projeto com id = {request.Id}.");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}