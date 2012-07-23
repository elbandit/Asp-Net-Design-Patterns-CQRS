using NHibernate;

namespace Agathas.Storefront.Infrastructure.Repositories
{
    public interface ISessionCoordinator
    {
        ISession get_current_session();
    }
}
