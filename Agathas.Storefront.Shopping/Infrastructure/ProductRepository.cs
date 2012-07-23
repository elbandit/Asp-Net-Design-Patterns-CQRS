using Agathas.Storefront.Infrastructure.Repositories;
using Agathas.Storefront.Shopping.Model.Baskets.Products;

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