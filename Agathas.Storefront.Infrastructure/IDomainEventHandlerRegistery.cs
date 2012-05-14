namespace Agathas.Storefront.Infrastructure
{
    public interface IDomainEventHandlerRegistery
    {
        void handle<TEvent>(TEvent domain_event) where TEvent : IDomainEvent;        
    }
}