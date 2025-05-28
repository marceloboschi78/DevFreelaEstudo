using DevFreela.Application.Models;
using DevFreela.Application.Notification;
using DevFreela.Infraestructure.Persistence;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectInsertCommandHandler : IRequestHandler<ProjectInsertCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        private readonly IMediator _mediator;

        public ProjectInsertCommandHandler(DevFreelaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ResultViewModel<int>> Handle(ProjectInsertCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreated, cancellationToken);

            int idProject = project.Id;

            return ResultViewModel<int>.Success(idProject);
        }
    }
}