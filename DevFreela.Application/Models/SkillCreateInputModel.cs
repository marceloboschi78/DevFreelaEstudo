using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class SkillCreateInputModel
    {
        public string Description { get; set; }

        public Skill ToEntity()
        {
            return new Skill(Description);
        }
    }
}
