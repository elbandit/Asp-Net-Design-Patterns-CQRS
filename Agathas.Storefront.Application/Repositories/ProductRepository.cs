using Agathas.Storefront.Sales.Model.Products;

namespace Agathas.Storefront.Sales.Api.Repositories
{
    public class ProductRepository : NhRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ISessionCoordinator session)
            : base(session)
        {
        }
    }
}