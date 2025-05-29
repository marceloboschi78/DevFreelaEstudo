using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.CQRS.Commands
{
    public class SkillInsertCommandHandler : IRequestHandler<SkillInsertCommand, ResultViewModel<string>>
    {
        private readonly ISkillRepository _repository;
        public SkillInsertCommandHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<string>> Handle(SkillInsertCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();

            await _repository.Add(skill);            

            return ResultViewModel<string>.Success(skill.Description);
        }        
    }
}
