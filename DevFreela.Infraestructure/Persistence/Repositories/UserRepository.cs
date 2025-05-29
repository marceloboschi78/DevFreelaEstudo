using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infraestructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;

        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> AddSkill(List<UserSkill> userSkills)
        {
            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();

            return userSkills[0].IdUser;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserSkills)
                    .ThenInclude(us => us.Skill)
                .SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<string> ProfilePicture(IFormFile file, int idUser)
        {
            var description = $"File: {file.FileName} - Size: {file.Length} bytes";
            
            //processar imagem
            
            return description;
        }
    }
}
