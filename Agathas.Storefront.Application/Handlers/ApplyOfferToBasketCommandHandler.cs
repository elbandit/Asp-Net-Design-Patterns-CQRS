using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Sales.Model.Baskets;
using Agathas.Storefront.Shopping.Model.Baskets;
using Agathas.Storefront.Shopping.Model.Coupons;

namespace Agathas.Storefront.Sales.Api.Handlers
{
    public class ApplyOfferToBasketCommandHandler : ICommandHandler<ApplyOfferToBasketCommand>
    {
        private readonly IOfferRepository _offer_repository;
        private readonly IBasketRepository _basket_repository;


        public ApplyOfferToBasketCommandHandler(IOfferRepository offer_repository,
                                                IBasketRepository basket_repository)
        {
            _offer_repository = offer_repository;
            _basket_repository = basket_repository;
        }

        public void action(ApplyOfferToBasketCommand business_request)
        {
            var offer = _offer_repository.find_by(business_request.voucher_id);

            if (offer.is_still_active())
            {
                var basket_discount_voucher = offer.create_discount_voucher_for(business_request.basket_id);
                var basket = _basket_repository.find_by(business_request.basket_id);

                basket.apply(basket_discount_voucher);

                _basket_repository.add(basket);
            }
        }
    }
}