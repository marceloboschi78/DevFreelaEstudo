using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infraestructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _context;

        public SkillRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<string> Add(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();

            return skill.Description;
        }

        public async Task<List<Skill>> GetAll()
        {
            var skills = await _context.Skills
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            return skills;
        }
    }
}
