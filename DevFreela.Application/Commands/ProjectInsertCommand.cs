using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class ProjectInsertCommand : IRequest<ResultViewModel<int>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public decimal TotalCost { get; set; }

        public Project ToEntity()
        {
            return new Project(Title, Description, IdClient, IdFreelancer, TotalCost);
        }
    }
}