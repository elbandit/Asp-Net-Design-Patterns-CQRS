using System;
using System.Collections.Generic;
using Agathas.Storefront.Infrastructure;
using Machine.Specifications;

namespace Agathas.Storefront.Specs.Core.Domain
{
    public class DomainEventsContext : IDomainEventHandlerRegistery
    {
        public IList<IDomainEvent> Events = new List<IDomainEvent>();

        public void handle<TEvent>(TEvent domain_event) where TEvent : IDomainEvent
        {
            Events.Add(domain_event);
        }
    }
}