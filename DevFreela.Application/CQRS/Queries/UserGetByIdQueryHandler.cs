using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Queries
{
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, ResultViewModel<UserViewModel>>
    {
        private readonly DevFreelaDbContext _context;
        public UserGetByIdQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<UserViewModel>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.UserSkills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefaultAsync(u => u.Id == request.Id);

            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error($"Não foi encontrado usuário com id = {request.Id}.");
            }

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);
        }
    }
}