using Azure.Core;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class ProjectInsertCommentCommand : IRequest<ResultViewModel<string>>
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}