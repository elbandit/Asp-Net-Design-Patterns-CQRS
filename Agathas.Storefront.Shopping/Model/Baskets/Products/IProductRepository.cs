using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Model.Baskets.Products
{
    public interface IProductRepository : IRepository<Product, int>
    {
        //Product find_by(int productid);
    }
}
