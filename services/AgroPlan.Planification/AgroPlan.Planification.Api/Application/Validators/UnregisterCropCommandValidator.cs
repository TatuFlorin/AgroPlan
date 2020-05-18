using System;
using AgroPlan.Planification.Api.Application.Commands;
using FluentValidation;

namespace AgroPlan.Planification.Api.Application.Validators
{
    public sealed class UnregisterCropCommandValidator : AbstractValidator<UnregisterCropCommand>
    {
        public UnregisterCropCommandValidator()
        {
            RuleFor(x => x.CropId)
                .Must(x => x.GetType() == typeof(Guid))
                .Must(x => x != Guid.Empty);

            RuleFor(x => x.PlanificationId)
                .Must(x => x.GetType() == typeof(Guid))
                .Must(x => x != Guid.Empty);
        }
    }
}