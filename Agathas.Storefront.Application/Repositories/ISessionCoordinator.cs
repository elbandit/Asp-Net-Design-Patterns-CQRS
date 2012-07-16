using NHibernate;

namespace Agathas.Storefront.Sales.Api.Repositories
{
    public interface ISessionCoordinator
    {
        ISession get_current_session();
    }
}
