using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace Agathas.Storefront.Infrastructure.Repositories
{
    public class NhRepository<T, TKey> : IRepository<T, TKey>
    {
        private readonly ISessionCoordinator _session;

        public NhRepository(ISessionCoordinator session)
        {
            _session = session;
        }
       
        public T find_by(TKey id)
        {
            return _session.get_current_session().Get<T>(id);
        }

        public void add(T entity)
        {
            _session.get_current_session().Save(entity);
        }
    
        public void save(T entity)
        {
            _session.get_current_session().SaveOrUpdate(entity);
        }

        public T query_for_single(Func<T, bool> func)
        {
            return _session.get_current_session().Query<T>().Where(func).SingleOrDefault();
        }

        public List<T> query_for_list(Func<T, bool> func)
        {
            return _session.get_current_session().Query<T>().Where(func).ToList();
        }    
    }
}