using System;
using System.Linq;
using System.Text;
using Agathas.Storefront.Common;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Baskets;
using Agathas.Storefront.Shopping.Baskets.Events;
using Agathas.Storefront.Shopping.Model.Baskets;
using Agathas.Storefront.Shopping.Model.Baskets.Products;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Model;
using ProductSnapshot = Agathas.Storefront.Shopping.Model.Baskets.Products.ProductSnapshot;

namespace Agathas.Storefront.Specs.Core.Domain
{
    public class BasketSpecs
    {
        [Subject(typeof(Basket))]
        public class when_adding_products_to_a_basket : WithSubject<Basket>
        {
            private Establish context = () =>
            {
                _event_context = new DomainEventsContext();
                DomainEvents.set_domain_event_handler_registery(_event_context);

                Subject = new Basket(Guid.NewGuid());
                _productSnapshot = new ProductSnapshot(1, "Hat", new Money(5m), "Hats");
            };

            private Because of = () => Subject.add(_productSnapshot, null);

            private It should_raise_an_event_showing_that_the_total_cost_of_the_basket_has_increased = () =>
            {
                _event_context.Events.ShouldContain(x => x.GetType() == typeof(BasketPriceChanged));
            };
                        
            private static ProductSnapshot _productSnapshot;
            private static DomainEventsContext _event_context;
        }
    }
}
