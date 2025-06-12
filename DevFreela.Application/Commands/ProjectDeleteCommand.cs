using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectDeleteCommand : IRequest<ResultViewModel>
    {
        public ProjectDeleteCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}