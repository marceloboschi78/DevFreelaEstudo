﻿using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class SkillCreateInputModel
    {
        public string Description { get; set; }

        public Skill ToEntity()
        {
            return new Skill(Description);
        }
    }
}
