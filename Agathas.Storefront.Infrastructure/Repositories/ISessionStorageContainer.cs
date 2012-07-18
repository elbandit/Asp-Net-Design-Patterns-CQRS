using NHibernate;

namespace Agathas.Storefront.Infrastructure.Repositories
{
    public interface ISessionStorageContainer
    {
        ISession GetCurrentSession();
        void Store(ISession session);
    }
}