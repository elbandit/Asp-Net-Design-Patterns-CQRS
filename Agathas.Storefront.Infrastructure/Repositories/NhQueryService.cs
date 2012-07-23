using System;
using System.Linq;
using NHibernate.Linq;

namespace Agathas.Storefront.Infrastructure.Repositories
{
    public class NhQueryService : IQueryService
    {
        private readonly ISessionCoordinator _session_coordinator;

        public NhQueryService(ISessionCoordinator session_coordinator)
        {
            _session_coordinator = session_coordinator;
        }

        public T query_for_single<T>(Func<T, bool> query)
        {          
           return _session_coordinator.get_current_session().Query<T>().Where(query).FirstOrDefault();            
        }

        public T query_for_single<T>(object id)
        {
            return _session_coordinator.get_current_session().Get<T>(id);
        }
    }
}