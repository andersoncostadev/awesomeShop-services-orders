using AwesomeShop.Services.Orders.Core.Events;
using System;
using System.Collections.Generic;

namespace AwesomeShop.Services.Orders.Core.Entities
{
    public class AggregateRooot : IEntityBase
    {
        private List<IDomainEvent> _events = new();

        public Guid Id { get; protected set; }

        public IEnumerable<IDomainEvent> Events => _events;

        protected void AddEvent(IDomainEvent @event)
        {
            _events ??= new List<IDomainEvent>();

            _events.Add(@event);
        }
    }
}
