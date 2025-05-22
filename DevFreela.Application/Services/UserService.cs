using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _context;

        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.UserSkills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error($"Não foi encontrado usuário com id = {id}.");
            }

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(UserCreateInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();

            var IdUser = user.Id;

            return ResultViewModel<int>.Success(IdUser);
        }

        public ResultViewModel<int> InsertSkill(int id, UserSkillCreateInputModel model)
        {
            var userSkills = model.SkillIds
                .Select(skillId => new UserSkill(id, skillId))                                
                .ToList();           

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();            

            return ResultViewModel<int>.Success(model.IdUser);
        }

        public ResultViewModel<string> ProfilePicture(int id, IFormFile file)
        {
            var description = $"File: {file.FileName} - Size: {file.Length} bytes";

            //processar imagem

            return ResultViewModel<string>.Success(description);
        }
    }
}
