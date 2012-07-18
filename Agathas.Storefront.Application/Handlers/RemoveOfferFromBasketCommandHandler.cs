using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Shopping.Baskets;

namespace Agathas.Storefront.Application.Handlers
{
    public class RemoveOfferFromBasketCommandHandler : ICommandHandler<RemoveOfferFromBasketCommand>
    {
        private readonly IBasketRepository _basket_repository;

        public RemoveOfferFromBasketCommandHandler(IBasketRepository basket_repository)
        {
            _basket_repository = basket_repository;
        }

        public void action(RemoveOfferFromBasketCommand business_request)
        {
            var basket = _basket_repository.find_by(business_request.basket_id);

            basket.remove_offer_voucher();
        }
    }
}