using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Model.Baskets.Events;
using Agathas.Storefront.Shopping.Baskets.Events;
using Agathas.Storefront.Ui.Web.Views.View;

namespace Agathas.Storefront.Ui.Web.Views.EventHandlers
{
    public class BasketCreatedHandler : IDomainEventHandler<BasketCreated>
    {
        private readonly IDomainViewsRepository _domainViewsRepository;

        public BasketCreatedHandler(IDomainViewsRepository domainViewsRepository)
        {
            _domainViewsRepository = domainViewsRepository;
        }

        public void action(BasketCreated business_event)
        {
            // Update Read Model
            var basket_detail_view = new BasketDetailView();

            basket_detail_view.id = business_event.Id;
            basket_detail_view.amount_to_pay = business_event.AmountToPay.ToString();

            _domainViewsRepository.save(basket_detail_view);
        }
    }
}