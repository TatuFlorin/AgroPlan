using MediatR;
using AgroPlan.Property.AgroPlan.Property.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task TriggerDomainEvents(this IMediator mediator, PropertyContext context)
        {
            var entities = context.ChangeTracker
                .Entries<Entity<string>>()
                .Where(x => x.Entity.EventList != null && x.Entity.EventList.Any())
                .ToList();

            var events = entities
                .SelectMany(x => x.Entity.EventList)
                .ToList();

            entities.ToList().ForEach(x => x.Entity.ClearEvents());

            foreach(var @event in events)
            {
                await mediator.Publish(@event);
            }
        }
    }
}