namespace Agathas.Storefront.Infrastructure
{
    public interface ICommandHandler<T> where T : IBusinessRequest
    {        
        void action(T business_request);
    }
}