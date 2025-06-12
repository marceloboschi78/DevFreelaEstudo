using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectCompleteCommand : IRequest<ResultViewModel>
    {
        public ProjectCompleteCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}