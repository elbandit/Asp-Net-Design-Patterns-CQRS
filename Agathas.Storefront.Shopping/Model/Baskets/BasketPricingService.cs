using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Common;
using Agathas.Storefront.Shopping.Model.Coupons;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public class BasketPricingService : IBasketPricingService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ICouponPolicy _couponPolicy;
        private readonly IOfferRepository _offerRepository;

        public BasketPricingService(IBasketRepository basketRepository, 
                                    ICouponPolicy coupon_policy)
        {
            _basketRepository = basketRepository;
            _couponPolicy = coupon_policy;
        }

        // Side effect free function
        public BasketPricingBreakdown calculate_total_price_for(IEnumerable<BasketItem> items, IEnumerable<string> coupon_ids)
        {
            var basketPricingBreakdown = new BasketPricingBreakdown();            
            var basket_discount = new Money();
            var coupon_issue = CouponIssues.NotApplied;

            foreach (var coupon_id in coupon_ids)
            {
                // 1. Get all coupons associted with the basket
                // 2. Check if it is applicable or which one wins.
                var coupon = _offerRepository.find_by(coupon_id);

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

            basketPricingBreakdown.basket_total = items.Sum(x => x.line_total());
            basketPricingBreakdown.discount = basket_discount;
            basketPricingBreakdown.coupon_message = coupon_issue;

            return basketPricingBreakdown;
        }
    }
}
