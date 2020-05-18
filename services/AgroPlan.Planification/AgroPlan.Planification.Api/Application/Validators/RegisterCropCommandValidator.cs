using System;
using AgroPlan.Planification.Api.Application.Commands;
using FluentValidation;

namespace AgroPlan.Planification.Api.Application.Validators
{
    public sealed class RegisterCropCommandValidator : AbstractValidator<RegisterCropCommand>
    {
        public RegisterCropCommandValidator()
        {
            RuleFor(x => x.PlanificationId)
                .Must(x => x.GetType() == typeof(Guid))
                .Must(x => x != Guid.Empty);

            RuleFor(x => x.Surface)
                .NotNull()
                .LessThanOrEqualTo(0);

            RuleFor(x => x.CropCode)
                .NotNull()
                .LessThanOrEqualTo(0);

            RuleFor(x => x.ParcelCode)
                .NotNull()
                .LessThanOrEqualTo(0);

            RuleFor(x => x.PhysicalBlockCode)
                .NotNull()
                .LessThanOrEqualTo(0);
        }
    }
}