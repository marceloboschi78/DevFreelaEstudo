using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectDeleteCommandHandler : IRequestHandler<ProjectDeleteCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        
        public ProjectDeleteCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {request.Id}.");
            }

            project.SetAsDeleted();

            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}