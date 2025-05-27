using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.CQRS.Queries
{
    public class ProjectGetAllQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public ProjectGetAllQuery(string search, int page, int size)
        {
            Search = search;
            Page = page;
            Size = size;
        }

        public string Search { get; set; } = string.Empty;
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
