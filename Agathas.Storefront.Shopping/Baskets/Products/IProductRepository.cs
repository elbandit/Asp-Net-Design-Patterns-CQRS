using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Sales.Model.Products
{
    public interface IProductRepository : IRepository<Product, int>
    {
        //Product find_by(int productid);
    }
}
