﻿using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAll();
        Task<string> Add(Skill skill);
    }
}
