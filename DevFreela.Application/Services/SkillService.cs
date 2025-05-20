using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;
        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<List<SkillViewModel>> GetAll()
        {
            var skills = _context.Skills
                .Where(s => !s.IsDeleted)
                .ToList();

            var model = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }
        public ResultViewModel<string> Insert(SkillCreateInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<string>.Success(skill.Description);
        }
    }
}
