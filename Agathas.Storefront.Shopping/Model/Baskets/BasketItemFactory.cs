using Agathas.Storefront.Shopping.Baskets;
using Agathas.Storefront.Shopping.Model.Baskets.Products;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public static class BasketItemFactory
    {
        public static BasketItem create_item_for(Product product)
        {
            return new BasketItem(product, new NonNegativeQuantity(1));
        }
    }
}