namespace Agathas.Storefront.Infrastructure
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void action(T business_event);
    }
}