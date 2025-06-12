using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries
{
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, ResultViewModel<UserViewModel>>
    {
        private readonly IUserRepository _repository;        
        public UserGetByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<UserViewModel>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);            

            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error($"Não foi encontrado usuário com id = {request.Id}.");
            }

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);
        }
    }
}