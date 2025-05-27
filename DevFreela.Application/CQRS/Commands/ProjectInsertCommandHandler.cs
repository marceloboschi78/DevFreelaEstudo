using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectInsertCommandHandler : IRequestHandler<ProjectInsertCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;

        public ProjectInsertCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(ProjectInsertCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            int idProject = project.Id;

            return ResultViewModel<int>.Success(idProject);
        }
    }
}