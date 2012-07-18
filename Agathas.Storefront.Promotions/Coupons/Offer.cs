using System;
using Agathas.Storefront.Common;
using Agathas.Storefront.Sales.Model.Offers;

namespace Agathas.Storefront.Promotions.Coupons
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
    }
}