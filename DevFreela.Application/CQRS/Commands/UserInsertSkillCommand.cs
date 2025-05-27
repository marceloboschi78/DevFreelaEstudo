using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class UserInsertSkillCommand : IRequest<ResultViewModel<int>>
    {
        public int[] SkillIds { get; set; }
        public int IdUser { get; set; }

        public UserSkill ToEntity()
        {
            return new UserSkill(IdUser, SkillIds[0]);
        }
    }
}
