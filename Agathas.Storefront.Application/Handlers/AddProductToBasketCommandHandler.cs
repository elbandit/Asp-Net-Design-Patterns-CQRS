using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Shopping.Model.Baskets;
using Agathas.Storefront.Shopping.Model.Baskets.Products;

namespace Agathas.Storefront.Application.Handlers
{
    public class AddProductToBasketCommandHandler : ICommandHandler<AddProductToBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _product_repository;
        private readonly IBasketPricingService _basketPricingService;

        public AddProductToBasketCommandHandler(IBasketRepository basketRepository,
                                                IProductRepository product_repository,
                                                IBasketPricingService basket_pricing_service)
        {
            _basketRepository = basketRepository;
            _product_repository = product_repository;
            _basketPricingService = basket_pricing_service;
        }

        public void action(AddProductToBasketCommand business_request)
        {
            var basket = _basketRepository.find_by(business_request.basket_id);            

            var product = _product_repository.find_by(business_request.productid);

            basket.add(product, _basketPricingService);                                    
        }
    }
}