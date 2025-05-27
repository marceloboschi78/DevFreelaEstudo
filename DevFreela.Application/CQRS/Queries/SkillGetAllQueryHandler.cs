using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.CQRS.Queries
{
    public class SkillGetAllQueryHandler : IRequestHandler<SkillGetAllQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly DevFreelaDbContext _context;
        public SkillGetAllQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(SkillGetAllQuery request, CancellationToken cancellationToken)
        {
            var skills = await _context.Skills
                .Where(s => !s.IsDeleted)
                .ToListAsync();

            var model = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();
                        
            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }        
    }
}
