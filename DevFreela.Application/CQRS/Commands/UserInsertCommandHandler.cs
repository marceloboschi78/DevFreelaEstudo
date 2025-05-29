using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class UserInsertCommandHandler : IRequestHandler<UserInsertCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _repository;

        public UserInsertCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(UserInsertCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            await _repository.Add(user);

            var IdUser = user.Id;

            return ResultViewModel<int>.Success(IdUser);
        }
    }
}
