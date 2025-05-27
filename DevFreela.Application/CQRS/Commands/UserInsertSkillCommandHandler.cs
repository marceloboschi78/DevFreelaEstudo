using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class UserInsertSkillCommandHandler : IRequestHandler<UserInsertSkillCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public UserInsertSkillCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(UserInsertSkillCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds
                .Select(skillId => new UserSkill(request.IdUser, skillId))
                .ToList();

            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(request.IdUser);
        }
    }
}
