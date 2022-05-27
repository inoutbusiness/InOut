using FluentValidation;
using InOut.Domain.Entities;

namespace InOut.Domain.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O email não pode ser nulo")
                .NotEmpty()
                .WithMessage("O email não pode ser vázio.")
                .MinimumLength(6)
                .WithMessage("O email deve ter no mínimo 6 caractéres.")
                .MaximumLength(180)
                .WithMessage("O email não pode ter mais de 180 caractéres.")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email inválido.");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("A senha não pode ser nulo")
                .NotEmpty()
                .WithMessage("A senha não pode ser vázio.")
                .MinimumLength(3)
                .WithMessage("A senha deve ter no mínimo 3 caractéres.")
                .MaximumLength(80)
                .WithMessage("A senha não pode ter mais de 80 caractéres.");
        }
    }
}