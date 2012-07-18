using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Baskets.Products;

namespace Agathas.Storefront.Sales.Model.Products
{
    public interface IProductRepository : IRepository<Product, int>
    {
        //Product find_by(int productid);
    }
}
