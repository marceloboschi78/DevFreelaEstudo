using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries
{
    public class ProjectGetByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
    {
        public ProjectGetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}