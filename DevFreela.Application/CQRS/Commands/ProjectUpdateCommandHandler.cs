using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectUpdateCommandHandler : IRequestHandler<ProjectUpdateCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;

        public ProjectUpdateCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {request.IdProject}.");
            }

            project.Update(request.Title, request.Description, request.TotalCost);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}