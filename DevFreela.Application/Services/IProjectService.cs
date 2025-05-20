using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string search, int page, int size);
        ResultViewModel<ProjectViewModel> GetById(int id);
        ResultViewModel<int> Insert(ProjectCreateInputModel model);
        ResultViewModel Update(int id, ProjectUpdateInputModel model);
        ResultViewModel Delete(int id);
        ResultViewModel Start(int id);
        ResultViewModel Complete(int id);
        ResultViewModel<string> InsertComment(int id, ProjectCommentCreateInputModel model);        
    }
}
