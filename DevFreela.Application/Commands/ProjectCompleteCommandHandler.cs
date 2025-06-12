using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectCompleteCommandHandler : IRequestHandler<ProjectCompleteCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        
        public ProjectCompleteCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(ProjectCompleteCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {request.Id}.");
            }

            project.Complete();

            await _repository.Update(project);            

            return ResultViewModel.Success();
        }
    }
}