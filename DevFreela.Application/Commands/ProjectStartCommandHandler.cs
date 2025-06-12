using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands
{
    public class ProjectStartCommandHandler : IRequestHandler<ProjectStartCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public ProjectStartCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(ProjectStartCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error($"Não encontrado projeto com id = {request.Id}.");
            }

            project.Start();

            await _repository.Update(project);            

            return ResultViewModel.Success();
        }
    }
}