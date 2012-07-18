using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Model.Baskets.Events;
using Agathas.Storefront.Shopping.Baskets.Events;
using Agathas.Storefront.Ui.Web.Views.View;

namespace Agathas.Storefront.Ui.Web.Views.EventHandlers
{
    // Application Handler

    public class BasketPriceChangedHandler : IDomainEventHandler<BasketPriceChanged>
    {
        private readonly IDomainViewsRepository _domainViewsRepository;
        private readonly IQueryService _queryService;

        public BasketPriceChangedHandler(IDomainViewsRepository domainViewsRepository,
                                         IQueryService queryService)
        {
            _domainViewsRepository = domainViewsRepository;
            _queryService = queryService;
        }

        public void action(BasketPriceChanged business_event)
        {            
            var basket_detail_view = _queryService.query_for_single<BasketDetailView>(business_event.basket_id);
                
            if (basket_detail_view == null)
            {
                basket_detail_view = new BasketDetailView();
                basket_detail_view.id = business_event.basket_id;
            }
           

            basket_detail_view.amount_to_pay = business_event.CostOfBasket.ToString();

            _domainViewsRepository.save(basket_detail_view);
        }
    }
}