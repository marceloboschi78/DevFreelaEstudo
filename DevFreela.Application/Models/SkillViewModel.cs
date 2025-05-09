using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class SkillViewModel
    {
        public SkillViewModel(string description)
        {
            Description = description;
        }
        public string Description { get; private set; }

        public static SkillViewModel FromEntity(Skill entity)
        {
            return new SkillViewModel(entity.Description);
        }
    }
}
