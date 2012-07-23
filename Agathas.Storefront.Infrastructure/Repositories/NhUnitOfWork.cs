namespace Agathas.Storefront.Infrastructure.Repositories
{
    public class NhUnitOfWork :IUnitOfWork
    {
        private readonly ISessionCoordinator _session;

        public NhUnitOfWork(ISessionCoordinator session)
        {
            _session = session;
        }        

        public void Commit()
        {
            using (var trans = _session.get_current_session().BeginTransaction())
            {
                trans.Commit();
            }
        }

        public void Rollback()
        {
            
        }                     
    }
}