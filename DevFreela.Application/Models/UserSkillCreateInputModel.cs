using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class UserSkillCreateInputModel
    {
        public int[] SkillIds { get; set; }
        public int IdUser { get; set; }

        public UserSkill ToEntity()
        {
            return new UserSkill(IdUser, SkillIds[0]);
        }
    }
}
