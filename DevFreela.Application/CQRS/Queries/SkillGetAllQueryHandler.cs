using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.CQRS.Queries
{
    public class SkillGetAllQueryHandler : IRequestHandler<SkillGetAllQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly ISkillRepository _repository;        
        public SkillGetAllQueryHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(SkillGetAllQuery request, CancellationToken cancellationToken)
        {
            var skills = await _repository.GetAll();

            var model = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();
                        
            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }        
    }
}
