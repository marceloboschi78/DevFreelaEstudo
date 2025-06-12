using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class UserInsertSkillCommandHandler : IRequestHandler<UserInsertSkillCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _repository;
        public UserInsertSkillCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(UserInsertSkillCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds
                .Select(skillId => new UserSkill(request.IdUser, skillId))
                .ToList();

            await _repository.AddSkill(userSkills);            

            return ResultViewModel<int>.Success(userSkills[0].IdUser);
        }
    }
}
