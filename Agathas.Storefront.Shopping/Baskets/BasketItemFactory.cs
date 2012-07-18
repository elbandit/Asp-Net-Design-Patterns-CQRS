using Agathas.Storefront.Sales.Model.Baskets;
using Agathas.Storefront.Shopping.Baskets.Products;

namespace Agathas.Storefront.Shopping.Baskets
{
    public static class BasketItemFactory
    {
        public static BasketItem create_item_for(Product product)
        {
            return new BasketItem(product, new NonNegativeQuantity(1));
        }
    }
}