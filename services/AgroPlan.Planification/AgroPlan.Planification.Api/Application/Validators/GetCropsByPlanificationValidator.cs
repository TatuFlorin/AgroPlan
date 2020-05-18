using System;
using AgroPlan.Planification.Api.Application.Queries;
using FluentValidation;

namespace AgroPlan.Planification.Api.Application.Validators
{
    public sealed class GetCropsByPlanificationValidator : AbstractValidator<GetCropsByPlanificationQuery>
    {
        public GetCropsByPlanificationValidator()
        {
            RuleFor(x => x.PlanificationId)
                .Must(x => x.GetType() == typeof(Guid))
                .Must(x => x != Guid.Empty);
        }
    }
}