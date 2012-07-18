using System;
using Agathas.Storefront.Common;
using Agathas.Storefront.Sales.Model.Baskets;
using Agathas.Storefront.Shopping.Baskets;
using Agathas.Storefront.Shopping.Model.Baskets.Products;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    // The basket has no rules, or invariants, its just a list of product id's
    // 
    public class BasketItem 
    {
        private Guid _id;
        private Money _price; // should this be cost of item at time of adding to basket rather than actual product cost?
        private NonNegativeQuantity _quantity;
        private int _product_id;// Should be Product Snapshot
        private ProductSnapshot _product_snapshot;

        private BasketItem()
        {
        }

        public BasketItem(Product product, NonNegativeQuantity quantity)
        {
            _id = Guid.NewGuid();
            _price = product.price;
            _product_id = product.id;
            _quantity = quantity;
        }
        
        public Money line_total()
        {
            return _price.multiple_by(_quantity.value);
        }
                
        public bool contains(Product product)
        {
            return _product_id == product.id;
        }

        public bool contains(int product_id)
        {
            return _product_id == product_id;
        }

        public void increase_item_quantity_by(NonNegativeQuantity quantity)
        {
            _quantity = _quantity.Add(quantity);
        }

        public void change_item_quantity_to(NonNegativeQuantity quantity)
        {
            _quantity = quantity;
        }

        public bool contains_product_that_is_in_same_category_as(string category) // change to strongly typed Category
        {
            return true;
        }
    }
}