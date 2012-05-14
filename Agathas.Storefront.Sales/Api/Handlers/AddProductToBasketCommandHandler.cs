using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Sales.Model.Baskets;
using Agathas.Storefront.Sales.Model.Products;

namespace Agathas.Storefront.Sales.Api.Handlers
{
    public class AddProductToBasketCommandHandler : ICommandHandler<AddProductToBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _product_repository;

        public AddProductToBasketCommandHandler(IBasketRepository basketRepository,
                                                IProductRepository product_repository)
        {
            _basketRepository = basketRepository;
            _product_repository = product_repository;
        }

        public void action(AddProductToBasketCommand business_request)
        {
            var basket = _basketRepository.find_by(business_request.basket_id);            

            var product = _product_repository.find_by(business_request.productid);

            basket.add(product);     
       
            // re-calculate the basket cost and throw event?
            
        }
    }
}