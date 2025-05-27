using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectStartCommandHandler : IRequestHandler<ProjectStartCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;

        public ProjectStartCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(ProjectStartCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {request.Id}.");
            }

            project.Start();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}