using NHibernate;
using NHibernate.Cfg;

namespace Agathas.Storefront.Infrastructure.Repositories
{
    public class SessionFactory : ISessionCoordinator
    {
        private static ISessionFactory _SessionFactory;

        public static void Init()
        {
            Configuration config = new Configuration();
            config.AddAssembly("Agathas.Storefront.Shopping");

            config.Configure();

            _SessionFactory = config.BuildSessionFactory();
        }

        private static ISessionFactory GetSessionFactory()
        {
            if (_SessionFactory == null)
                Init();

            return _SessionFactory;
        }

        public static ISession GetNewSession()
        {
            return GetSessionFactory().OpenSession();
        }

        public ISession get_current_session()
        {
            ISessionStorageContainer _sessionStorageContainer = SessionStorageFactory.GetStorageContainer();

            ISession currentSession = _sessionStorageContainer.GetCurrentSession();

            if (currentSession == null)
            {
                currentSession = GetNewSession();
                _sessionStorageContainer.Store(currentSession);
            }

            return currentSession;
        }
        
    }
}