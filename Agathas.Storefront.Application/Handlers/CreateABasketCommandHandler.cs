using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Sales.Model.Baskets;

namespace Agathas.Storefront.Sales.Api.Handlers
{
    public class CreateABasketCommandHandler : ICommandHandler<CreateABasketCommand>
    {
        private readonly IBasketRepository _basket_repository;

        public CreateABasketCommandHandler(IBasketRepository basket_repository)
        {
            _basket_repository = basket_repository;
        }

        public void action(CreateABasketCommand business_request)
        {
            var basket = new Basket(business_request.basket_id);

            _basket_repository.add(basket);
        }
    }
}