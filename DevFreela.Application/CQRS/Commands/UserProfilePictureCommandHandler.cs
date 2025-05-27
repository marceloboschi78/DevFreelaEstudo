using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class UserProfilePictureCommandHandler : IRequestHandler<UserProfilePictureCommand, ResultViewModel<string>>
    {
        private readonly DevFreelaDbContext _context;

        public UserProfilePictureCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<string>> Handle(UserProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var description = $"File: {request.Picture.FileName} - Size: {request.Picture.Length} bytes";

            //processar imagem

            return ResultViewModel<string>.Success(description);
        }
    }
}
