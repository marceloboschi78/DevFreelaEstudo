using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {
        ResultViewModel<UserViewModel> GetById(int id);
        ResultViewModel<int> Insert(UserCreateInputModel model);
        ResultViewModel<int> InsertSkill(int id, UserSkillCreateInputModel model);
        ResultViewModel<string> ProfilePicture(int id, IFormFile file);
    }
}
