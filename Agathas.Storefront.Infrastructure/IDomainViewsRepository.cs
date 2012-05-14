namespace Agathas.Storefront.Infrastructure
{
    public interface IDomainViewsRepository
    {
        void save<TDomainView>(TDomainView domain_view) where TDomainView : IDomainView;
    }
}
