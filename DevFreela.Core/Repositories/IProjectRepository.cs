using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAll(int page, int size, string search);
        Task<Project> GetDetailsById(int id);
        Task<Project> GetById(int id);
        Task<int> Add(Project project);
        Task Update(Project project);
        Task AddComment(ProjectComment comment);
        Task<bool> Exists(int id);
    }
}
