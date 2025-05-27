using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.CQRS.Queries
{
    public class UserGetByIdQuery : IRequest<ResultViewModel<UserViewModel>>
    {
        public UserGetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}