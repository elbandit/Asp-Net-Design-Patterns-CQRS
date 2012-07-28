using Agathas.Storefront.Application.Commands;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Shopping.Model.Baskets;
using Agathas.Storefront.Shopping.Model.Coupons;

namespace Agathas.Storefront.Application.Handlers
{
    public class ApplyCouponToBasketCommandHandler : ICommandHandler<ApplyCouponToBasketCommand>
    {
        private readonly IOfferRepository _offer_repository;
        private readonly IBasketRepository _basket_repository;
        private readonly IBasketPricingService _basketPricingService;


        public ApplyCouponToBasketCommandHandler(IOfferRepository offer_repository,
                                                IBasketRepository basket_repository,
                                                IBasketPricingService basketPricingService)
        {
            _offer_repository = offer_repository;
            _basket_repository = basket_repository;
            _basketPricingService = basketPricingService;
        }

        public void action(ApplyCouponToBasketCommand business_request)
        {
            var coupon = _offer_repository.find_by(business_request.voucher_id);
            var basket = _basket_repository.find_by(business_request.basket_id);

            basket.apply(coupon, _basketPricingService);
            
            _basket_repository.save(basket);
        }
    }
}