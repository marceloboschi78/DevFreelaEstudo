using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries
{
    public class SkillGetAllQuery : IRequest<ResultViewModel<List<SkillViewModel>>>
    {

    }
}
