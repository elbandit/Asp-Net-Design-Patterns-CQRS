namespace Agathas.Storefront.Infrastructure
{
    public static class DomainEvents
    {
        private static IDomainEventHandlerRegistery _domain_event_handler_registery;

        public static void set_domain_event_handler_registery(IDomainEventHandlerRegistery domain_event_handler_registery)
        {
            _domain_event_handler_registery = domain_event_handler_registery;
        }

        public static void raise<T>(T domain_event) where T : IDomainEvent
        {
            _domain_event_handler_registery.handle(domain_event);
        }
    }
}