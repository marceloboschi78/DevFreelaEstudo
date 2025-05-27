using DevFreela.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevFreela.Application.CQRS.Commands
{
    public class UserProfilePictureCommand : IRequest<ResultViewModel<string>>
    {
        public UserProfilePictureCommand(int id, IFormFile picture)
        {
            Id = id;
            Picture = picture;
        }

        public int Id { get; set; }
        public IFormFile Picture { get; set; }
    }
}
