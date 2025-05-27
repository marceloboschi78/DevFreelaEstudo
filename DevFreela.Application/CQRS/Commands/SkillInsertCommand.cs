using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class SkillInsertCommand : IRequest<ResultViewModel<string>>
    {
        public string Description { get; set; }
        public Skill ToEntity()
        {
            return new Skill(Description);
        }
    }
}
