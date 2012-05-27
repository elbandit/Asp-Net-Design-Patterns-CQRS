using System;
using System.Collections.Generic;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Model.Baskets.Events;

namespace Agathas.Storefront.Sales.Model.Baskets
{
    public class BasketVoucher
    {
        private Guid _id;
        private Guid _basket_id; // should this be the id?
        private readonly string _voucher_code;
        private readonly Money _discount;
        private readonly Money _threshold;

        // _has_been_applied?
        // _reason_why_not_applied

        private BasketVoucher()
        {
            
        }

        public BasketVoucher(Guid basket_id, string voucher_code, Money discount, Money threshold)
        {
            _id = Guid.NewGuid();
            _basket_id = basket_id;
            _voucher_code = voucher_code;
            _discount = discount;
            _threshold = threshold;
        }


        public Guid basket_id
        {
            get { return _basket_id; }
        }

        public bool can_be_applied_to(Basket basket)
        {
            return !basket.has_had_vouchers_applied(); // && not_out_of_date
        }

        public bool meets_criteria_for_discount(Basket basket)
        {
            return basket.items_total.is_greater_than(_threshold);
        }

        public void determine_why_not_applicable_for(Basket basket)
        {
            if (basket.items_total.is_less_than(_threshold) || basket.items_total.Equals(_threshold))
            {
                DomainEvents.raise(
                    new VoucherNotApplicableForBasket(basket.id,
                                                      "Your basket is below the threshold in order to apply the voucher XXX-XXX."));
            }
        }
        
        public Money calculate_discount_for(Basket basket)
        {
            var discount = new Money();

            if (this.meets_criteria_for_discount(basket))
            {
                discount = discount.add(_discount);                
            }
            else
            {
                determine_why_not_applicable_for(basket); // This sets why the voucher is not applicable
            }

            return discount;
        }        
    }
}