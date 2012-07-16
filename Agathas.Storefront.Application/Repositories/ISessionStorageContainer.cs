using NHibernate;

namespace Agathas.Storefront.Sales.Api.Repositories
{
    public interface ISessionStorageContainer
    {
        ISession GetCurrentSession();
        void Store(ISession session);
    }
}