using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Common;
using Agathas.Storefront.Shopping.Model.Coupons;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public class BasketPricingService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOfferRepository _offerRepository;

        public BasketPricingService(IBasketRepository basketRepository, IOfferRepository offerRepository)
        {
            _basketRepository = basketRepository;
            _offerRepository = offerRepository;
        }

        // Side effect free function
        public BasketPricingBreakdown calculate_total_price_for(Guid basket_id)
        {
            var basketPricingBreakdown = new BasketPricingBreakdown();
            var basket = _basketRepository.find_by(basket_id);
            var basket_discount = new Money();
            var coupon_issue = CouponIssues.NotApplied;

            if (basket.has_had_coupon_applied())
            {
                var coupon = _offerRepository.find_by(basket.coupon_id);

                if (coupon.is_still_active() && coupon.can_be_applied_to(basket))
                {
                    basket_discount = coupon.calculate_discount_for(basket);
                    coupon_issue = CouponIssues.NoIssue;
                }
                else
                {
                    coupon_issue = coupon.reason_why_cannot_be_applied_to(basket);
                }                                
            }

            basketPricingBreakdown.basket_total = basket.items_total;
            basketPricingBreakdown.discount = basket_discount;
            basketPricingBreakdown.coupon_message = coupon_issue;

            return basketPricingBreakdown;
        }
    }
}
