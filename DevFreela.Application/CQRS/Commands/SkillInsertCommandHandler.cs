using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class SkillInsertCommandHandler : IRequestHandler<SkillInsertCommand, ResultViewModel<string>>
    {
        private readonly DevFreelaDbContext _context;
        public SkillInsertCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<string>> Handle(SkillInsertCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();

            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();

            return ResultViewModel<string>.Success(skill.Description);
        }        
    }
}
