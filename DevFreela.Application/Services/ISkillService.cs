using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<SkillViewModel>> GetAll();
        ResultViewModel<string> Insert(SkillCreateInputModel model);
    }
}
