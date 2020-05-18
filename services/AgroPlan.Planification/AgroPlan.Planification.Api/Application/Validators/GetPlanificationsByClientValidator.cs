using System;
using AgroPlan.Planification.Api.Application.Queries;
using FluentValidation;

namespace AgroPlan.Planification.Api.Application.Validators
{
    public sealed class GetPlanificationsByClientValidator : AbstractValidator<GetPlanificationsByClientQuery>
    {
        public GetPlanificationsByClientValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty();
        }
    }
}