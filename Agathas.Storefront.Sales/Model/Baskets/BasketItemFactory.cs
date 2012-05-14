using Agathas.Storefront.Sales.Model.Products;

namespace Agathas.Storefront.Sales.Model.Baskets
{
    public static class BasketItemFactory
    {
        public static BasketItem create_item_for(Product product)
        {
            return new BasketItem(product, new NonNegativeQuantity(1));
        }
    }
}