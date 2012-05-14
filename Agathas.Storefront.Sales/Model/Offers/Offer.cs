using System;
using Agathas.Storefront.Sales.Model.Baskets;

namespace Agathas.Storefront.Sales.Model.Offers
{
    public class Offer
    {
        private readonly string _voucher_code;
        private readonly Money _discount;
        private readonly Money _threshold;
        private readonly Guid _id;

        private Offer()
        {
            _id = Guid.NewGuid();
        }

        public Offer(string voucher_code, Money discount, Money threshold) : this ()
        {
            // TODO: Check for null values and invalid data
            _voucher_code = voucher_code;
            _discount = discount;
            _threshold = threshold;
        }

        public BasketVoucher create_discount_voucher_for(Guid basket_id)
        {
            if (!is_still_active())
                throw new OfferNotApplicableException("reason");

            return new BasketVoucher(basket_id, _voucher_code, _discount, _threshold);
        }

        public bool is_still_active()
        {
            return true;
        }

        public string voucher_code
        {
            get { return _voucher_code; }
        }

        public void apply_to(Basket basket)
        {
            var offer_is_applicable = false;
            // 1) has product in category
            basket.contains_product(i => i.contains_product_that_is_in_same_category_as(""));

            // 2) basket total is over the threshold
            // 3) and is in date 
            // 4) Basket doen't contain any other vouchers
            basket.has_had_vouchers_applied();

            if (offer_is_applicable)
                basket.apply_discount_value_of(_discount);
            else
            {
                basket.apply_discount_value_of(new Money());
                
                // raise new voucher not applicable event for read model?
            }

        }
    }
}