using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries
{
    public class ProjectGetAllQueryHandler : IRequestHandler<ProjectGetAllQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {        
        private readonly IProjectRepository _repository;
        public ProjectGetAllQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }        
public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(ProjectGetAllQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAll(request.Page, request.Size, request.Search);

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
