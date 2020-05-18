using FluentValidation;

using AgroPlan.Planification.Api.Application.Commands;

namespace AgroPlan.Planification.Api.Application.Validators
{
    public sealed class MakePlanificationCommandValidator : AbstractValidator<MakePlanificationCommand>
    {
        public MakePlanificationCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty()
                .MinimumLength(13)
                .MaximumLength(13);
        }

    }
}