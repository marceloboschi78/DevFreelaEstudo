using DevFreela.API.Entities;

namespace DevFreela.API.Models
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
