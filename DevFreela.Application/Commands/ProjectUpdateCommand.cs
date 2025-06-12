using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectUpdateCommand : IRequest<ResultViewModel>
    {
        public int IdProject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
    }
}