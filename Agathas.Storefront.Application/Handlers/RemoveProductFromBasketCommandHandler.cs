using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Shopping.Model.Baskets;

namespace Agathas.Storefront.Application.Handlers
{
    public class RemoveProductFromBasketCommandHandler : ICommandHandler<RemoveProductFromBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public RemoveProductFromBasketCommandHandler(IBasketRepository basket_repository)
        {
            _basketRepository = basket_repository;
        }

        public void action(RemoveProductFromBasketCommand business_request)
        {
            var basket = _basketRepository.find_by(business_request.get_basket_id);

            basket.remove_product_with_id_of(business_request.product_id);
        }
    }
}