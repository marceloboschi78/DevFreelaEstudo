using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectInsertCommentCommandHandler : IRequestHandler<ProjectInsertCommentCommand, ResultViewModel<string>>
    {
        private readonly DevFreelaDbContext _context;

        public ProjectInsertCommentCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<string>> Handle(ProjectInsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (project is null)
            {
                return ResultViewModel<string>.Error($"Não encontrado projeto com id = {request.IdProject}.");
            }

            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return ResultViewModel<string>.Success(comment.Content);
        }
    }
}