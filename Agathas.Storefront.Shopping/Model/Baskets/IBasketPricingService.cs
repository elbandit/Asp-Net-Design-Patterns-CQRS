using System;
using System.Collections.Generic;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public interface IBasketPricingService
    {
        BasketPricingBreakdown calculate_total_price_for(IEnumerable<BasketItem> items, string coupon_id);
    }
}