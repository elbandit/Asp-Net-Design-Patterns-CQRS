namespace Agathas.Storefront.Infrastructure.Repositories
{
    public class DomainViewsRepository : IDomainViewsRepository
    {
        private readonly ISessionCoordinator _session;

        public DomainViewsRepository(ISessionCoordinator session)
        {
            _session = session;
        }

        public void save<TDomainView>(TDomainView domain_view) where TDomainView : IDomainView
        {
            _session.get_current_session().SaveOrUpdate(domain_view);
        }
        
    }
}