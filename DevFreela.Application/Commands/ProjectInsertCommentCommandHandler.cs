using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectInsertCommentCommandHandler : IRequestHandler<ProjectInsertCommentCommand, ResultViewModel<string>>
    {
        private readonly IProjectRepository _repository;
        
        public ProjectInsertCommentCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<string>> Handle(ProjectInsertCommentCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddComment(new ProjectComment(request.Content, request.IdProject, request.IdUser));

            return ResultViewModel<string>.Success(request.Content);
        }
    }
}