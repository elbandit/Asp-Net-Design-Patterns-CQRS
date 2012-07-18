using System;
using System.Collections.Generic;
using System.Linq;
using Agathas.Storefront.Common;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Baskets;
using Agathas.Storefront.Shopping.Baskets.Events;
using Agathas.Storefront.Shopping.Model.Baskets.Products;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public class Basket 
    {
        private IList<BasketItem> _items;
        private Guid _id;        
        private BasketVoucher _basket_voucher;
        
        private Basket()
        {
        }

        public Basket(Guid id)
        {
            _id = id;
            _items = new List<BasketItem>();            

            DomainEvents.raise(new BasketCreated(this._id, amount_to_pay()));
        }

        public Guid id
        {
            get { return _id; }
        }

        public Money items_total
        {
            get
            {
                var running_total = new Money();

                foreach (var line_item in _items)
                {
                    running_total = running_total.add(line_item.line_total());
                }

                return running_total;
            }
        }

        public string coupon_id
        {
            get { throw new NotImplementedException(); }
        }

        public void add(Product product)
        {
            // TODO: Check for null values and invalid data

            if (basket_contains_an_item_for(product))
                get_item_for(product).increase_item_quantity_by(new NonNegativeQuantity(1));
            else
                _items.Add(BasketItemFactory.create_item_for(product));

            DomainEvents.raise(new BasketPriceChanged(this._id, amount_to_pay()));
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

        public void remove_product_with_id_of(int product_id)
        {
            if (basket_contains_an_item_for(product_id))
            {
                _items.Remove(get_item_for(product_id));

                DomainEvents.raise(new BasketPriceChanged(this._id, amount_to_pay()));
            }
            
        }

        public void change_quantity_of_product(NonNegativeQuantity quantity, Product product)
        {
            // TODO: Check for null values and invalid data

            if (basket_contains_an_item_for(product))
            {
                if (quantity.is_zero())
                {
                    remove_product_with_id_of(product.id);
                }
                else
                    get_item_for(product).change_item_quantity_to(quantity);

                DomainEvents.raise(new BasketPriceChanged(this._id, amount_to_pay()));
            }
        }

       
        private Money calculate_cost_with_discount_of(Money discount)
        {            
            return items_total.minus(discount);
        }
                
        public Money amount_to_pay()
        {
            Money _discount = caclulate_basket_discount();

            return calculate_cost_with_discount_of(_discount);
        }

        private Money caclulate_basket_discount()
        {
            Money _discount = new Money();
            if (_basket_voucher != null)
                _discount = _basket_voucher.calculate_discount_for(this);            
            
            return _discount;
        }

        public void apply(BasketVoucher voucher)
        {
            if (voucher.can_be_applied_to(this))
            {
                this._basket_voucher = voucher;
                
                DomainEvents.raise(new BasketPriceChanged(this._id, amount_to_pay()));
            }           
        }

        public bool contains_product(Func<BasketItem, bool> func)
        {
            return _items.Any(func);
        }

        public bool has_had_vouchers_applied()
        {
            return this._basket_voucher != null;
        }

        public void remove_offer_voucher()
        {
            this._basket_voucher = null;

            DomainEvents.raise(new BasketPriceChanged(this._id, amount_to_pay()));
        }

        public bool has_had_coupon_applied()
        {
            throw new NotImplementedException();
        }
    }    
}
