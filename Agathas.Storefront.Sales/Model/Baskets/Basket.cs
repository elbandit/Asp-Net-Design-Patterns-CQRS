using System;
using System.Collections.Generic;
using System.Linq;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Model.Baskets.Events;
using Agathas.Storefront.Sales.Model.Products;

namespace Agathas.Storefront.Sales.Model.Baskets
{
    public class Basket 
    {
        private IList<BasketItem> _items;
        private Guid _id;
        private Money _discount;
        private BasketVoucher _basket_voucher;
        
        private Basket()
        {
        }

        public Basket(Guid id)
        {
            _id = id;
            _items = new List<BasketItem>();
            _discount = new Money();

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

        public void add(Product product)
        {
            // TODO: Check for null values and invalid data

            if (basket_contains_an_item_for(product))
                get_item_for(product).increase_item_quantity_by(new NonNegativeQuantity(1));
            else
                _items.Add(BasketItemFactory.create_item_for(product));

            recalculate_discount();
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

                recalculate_discount();
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

                recalculate_discount();
            }
        }

        private void recalculate_discount()
        {
            if (_basket_voucher != null)
                _basket_voucher.apply_to(this);
        }

        public Money calculate_cost_with_discount_of(Money discount)
        {
            // TODO: Check for null values and invalid data
            return items_total.minus(discount);
        }

        
        public void apply_discount_value_of(Money money)
        {
            // TODO: Check for null values and invalid data
            // Invariant: Can't apply more discount that total of items 
            _discount = money;
            
            DomainEvents.raise(new BasketPriceChanged(this._id , amount_to_pay()));
        }

        public Money amount_to_pay()
        {                        
            return calculate_cost_with_discount_of(_discount);
        }         

        public void apply(BasketVoucher voucher)
        {
            voucher.apply_to(this);

            if (voucher.is_applicable(this))
            {
                this._basket_voucher = voucher;
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
    }    
}
