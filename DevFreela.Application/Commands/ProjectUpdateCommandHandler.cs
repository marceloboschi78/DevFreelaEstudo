using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectUpdateCommandHandler : IRequestHandler<ProjectUpdateCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public ProjectUpdateCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.IdProject);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {request.IdProject}.");
            }

            project.Update(request.Title, request.Description, request.TotalCost);

            await _repository.Update(project);            

            return ResultViewModel.Success();
        }
    }
}