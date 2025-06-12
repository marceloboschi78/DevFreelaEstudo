using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class UserProfilePictureCommandHandler : IRequestHandler<UserProfilePictureCommand, ResultViewModel<string>>
    {
        private readonly IUserRepository _repository;

        public UserProfilePictureCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<string>> Handle(UserProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var description = await _repository.ProfilePicture(request.Picture, request.Id);            
             
            return ResultViewModel<string>.Success(description);
        }
    }
}
