using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectStartCommand : IRequest<ResultViewModel>
    {
        public ProjectStartCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}