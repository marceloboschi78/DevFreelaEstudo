using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class UserInsertCommandHandler : IRequestHandler<UserInsertCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;

        public UserInsertCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(UserInsertCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var IdUser = user.Id;

            return ResultViewModel<int>.Success(IdUser);
        }
    }
}
