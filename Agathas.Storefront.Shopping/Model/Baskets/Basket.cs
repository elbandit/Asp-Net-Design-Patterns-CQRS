using System;
using System.Collections.Generic;
using System.Linq;
using Agathas.Storefront.Common;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Baskets.Events;
using Agathas.Storefront.Shopping.Model.Baskets.Events;
using Agathas.Storefront.Shopping.Model.Baskets.Products;
using Agathas.Storefront.Shopping.Model.Coupons;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public class Basket 
    {
        private IList<BasketItem> _items;
        private string _associated_coupon;
        private Guid _id;        
               
        private Basket()
        {
        }

        public Basket(Guid id)
        {
            _id = id;
            _items = new List<BasketItem>();            

            DomainEvents.raise(new BasketCreated(this._id, new Money()));
        }

        public Guid id
        {
            get { return _id; }
        }
        
        public void add(Product product, IBasketPricingService basket_pricing_service)
        {
            // TODO: Check for null values and invalid data

            if (basket_contains_an_item_for(product))
                get_item_for(product).increase_item_quantity_by(new NonNegativeQuantity(1));
            else
                _items.Add(BasketItemFactory.create_item_for(product));

            recalculate_basket_totals(basket_pricing_service);
        }

        private BasketItem get_item_for(Product product)
        {
            return _items.Where(i => i.contains(product)).FirstOrDefault();
        }

        private BasketItem get_item_for(int product_id)
        {
            return _items.Where(i => i.contains(product_id)).FirstOrDefault();
        }

        private bool basket_contains_an_item_for(Product product)
        {
            return _items.Any(i => i.contains(product));
        }

        private bool basket_contains_an_item_for(int product_id)
        {
            return _items.Any(i => i.contains(product_id));
        }

        public void remove_product_with_id_of(int product_id, IBasketPricingService basket_pricing_service)
        {
            if (basket_contains_an_item_for(product_id))
            {
                _items.Remove(get_item_for(product_id));

                recalculate_basket_totals(basket_pricing_service);
            }
        }

        private void recalculate_basket_totals(IBasketPricingService basket_pricing_service)
        {
            var total = basket_pricing_service.calculate_total_price_for(_items, _coupon_id);

            DomainEvents.raise(new BasketPriceChanged(this._id, total.basket_total, total.discount));
        }

        public void change_quantity_of_product(NonNegativeQuantity quantity, Product product, IBasketPricingService basket_pricing_service)
        {
            // TODO: Check for null values and invalid data

            if (basket_contains_an_item_for(product))
            {
                if (quantity.is_zero())
                {
                    remove_product_with_id_of(product.id, basket_pricing_service);
                }
                else
                    get_item_for(product).change_item_quantity_to(quantity);

                recalculate_basket_totals(basket_pricing_service);
            }
        }
                      
        public bool contains_product(Func<BasketItem, bool> func)
        {
            return _items.Any(func);
        }

        public void apply(Offer coupon, IBasketPricingService basket_pricing_service)
        {            
            if (coupon.is_applicable_for(_items))
            {
                _associated_coupons.Add(coupon.id);
                recalculate_basket_totals(basket_pricing_service);
            }
            else            
                DomainEvents.raise(new CouponNotApplicableForBasketItems());            
        }

        public void remove_coupon(string couponCode, IBasketPricingService basketPricingService)
        {
            recalculate_basket_totals(basketPricingService);
        }
    }
}
