using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectInsertCommandValidateBehavior : IPipelineBehavior<ProjectInsertCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectInsertCommandValidateBehavior(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ResultViewModel<int>> Handle(ProjectInsertCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = _dbContext.Users.Any(u => u.Id == request.IdClient);
            var freelancerExists = _dbContext.Users.Any(u => u.Id == request.IdFreelancer);

            if (!clientExists || !freelancerExists)
            {
                return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos.");
            }

            return await next();
        }
    }
}
