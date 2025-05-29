using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
        Task<User> GetById(int id);
        Task<int> AddSkill(List<UserSkill> userSkills);
        Task<string> ProfilePicture(IFormFile file, int idUser);        
    }
}
