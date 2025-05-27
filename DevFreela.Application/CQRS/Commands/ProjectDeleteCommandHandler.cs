using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectDeleteCommandHandler : IRequestHandler<ProjectDeleteCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;

        public ProjectDeleteCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {request.Id}.");
            }

            project.SetAsDeleted();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}