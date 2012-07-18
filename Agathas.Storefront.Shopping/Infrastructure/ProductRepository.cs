using Agathas.Storefront.Sales.Api.Repositories;
using Agathas.Storefront.Sales.Model.Products;
using Agathas.Storefront.Shopping.Baskets.Products;

namespace Agathas.Storefront.Shopping.Infrastructure
{
    public class ProductRepository : NhRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ISessionCoordinator session)
            : base(session)
        {
        }
    }
}