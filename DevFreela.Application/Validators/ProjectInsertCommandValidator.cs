using DevFreela.Application.CQRS.Commands;
using FluentValidation;

namespace DevFreela.API.Validators
{
    public class ProjectInsertCommandValidator : AbstractValidator<ProjectInsertCommand>
    {
        public ProjectInsertCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("O título do projeto é obrigatório.")
                .MaximumLength(50).WithMessage("O título do projeto deve ter no máximo 50 caracteres.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("A descrição do projeto é obrigatória.")
                .MaximumLength(500).WithMessage("A descrição do projeto deve ter no máximo 500 caracteres.");
            RuleFor(p => p.TotalCost)
                .GreaterThan(1000).WithMessage("O custo total do projeto deve ser maior que R$1.000,00.");
            
        }
    }
}
