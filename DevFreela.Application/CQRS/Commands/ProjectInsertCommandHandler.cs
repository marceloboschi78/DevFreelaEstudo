using DevFreela.Application.Models;
using DevFreela.Application.Notification;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectInsertCommandHandler : IRequestHandler<ProjectInsertCommand, ResultViewModel<int>>
    {
        private readonly IProjectRepository _repository;
        private readonly IMediator _mediator;        

        public ProjectInsertCommandHandler(IMediator mediator, IProjectRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(ProjectInsertCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _repository.Add(project);

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreated, cancellationToken);

            int idProject = project.Id;

            return ResultViewModel<int>.Success(idProject);
        }
    }
}