using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.CQRS.Queries
{
    public class ProjectGetByIdQueryHandler : IRequestHandler<ProjectGetByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly IProjectRepository _repository;

        public ProjectGetByIdQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<ProjectViewModel>> Handle(ProjectGetByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetDetailsById(request.Id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Error($"Não encontrado projeto com id = {request.Id}.");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}