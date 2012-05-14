namespace Agathas.Storefront.Infrastructure
{
    public interface IUnitOfWork 
    {
        void Commit();
        void Rollback();
    }
}