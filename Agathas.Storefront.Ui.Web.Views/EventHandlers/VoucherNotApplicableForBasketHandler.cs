using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Model.Baskets.Events;
using Agathas.Storefront.Ui.Web.Views.View;

namespace Agathas.Storefront.Ui.Web.Views.EventHandlers
{
    public class VoucherNotApplicableForBasketHandler : IDomainEventHandler<VoucherNotApplicableForBasket>
    {
        private readonly IDomainViewsRepository _domainViewsRepository;
        private readonly IQueryService _query_service;

        public VoucherNotApplicableForBasketHandler(IDomainViewsRepository domainViewsRepository,
                                                    IQueryService queryService)
        {
            _domainViewsRepository = domainViewsRepository;
            _query_service = queryService;
        }

        public void action(VoucherNotApplicableForBasket business_event)
        {
            var basket_detail_view = _query_service.query_for_single<BasketDetailView>(business_event.basket_id);
            
            basket_detail_view.message = business_event.reason;

            _domainViewsRepository.save(basket_detail_view);
        }
    }
}