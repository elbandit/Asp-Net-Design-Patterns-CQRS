using Agathas.Storefront.Application;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Infrastructure.Repositories;
using Agathas.Storefront.Shopping.Infrastructure;
using Agathas.Storefront.Shopping.Model.Baskets;
using Agathas.Storefront.Shopping.Model.Baskets.Products;
using Agathas.Storefront.Shopping.Model.Promotions;
using Agathas.Storefront.Specs.Uat.Support;
using Chap2.ShoppingBasket.Ui.Web.Controllers;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Agathas.Storefront.Specs.Uat
{
    public static class BootStrapper
    {
        public static void configure_dependencies()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry<UiRegistry>();
                                             x.AddRegistry<CommandHandlersRegistry>();
                                             x.AddRegistry<EventHandlersRegistry>();                                                      
                                         });

            DomainEvents.set_domain_event_handler_registery(new DomainEventHandlerRegistery());

        }

        public class UiRegistry : Registry
        {
            public UiRegistry()
            {
                // Test
                For<ISessionCoordinator>().Use<TestNHConfigurator>();
                For<IClientStorage>().Use<InMemoryClientStorage>();

                // Production
                For<IBasketRepository>().Use<BasketRepository>();                
                For<IPromotionsRepository>().Use<PromotionsRepository>();
                For<IProductRepository>().Use<ProductRepository>();

                For<ICommandHandlerRegistry>().Use<CommandHandlerRegistry>();                
                For<IQueryService>().Use<NhQueryService>();
                For<IUnitOfWork>().Use<NhUnitOfWork>();
                For<IDomainViewsRepository>().Use<DomainViewsRepository>();
            }
        }

        public class CommandHandlersRegistry : Registry
        {
            public CommandHandlersRegistry()
            {
                Scan(s =>
                         {                                                          
                             s.Assembly("Agathas.Storefront.Application");
                             s.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                         });
            }
        }

        public class EventHandlersRegistry : Registry
        {
            public EventHandlersRegistry()
            {
                Scan(s =>
                {
                    s.Assembly("Agathas.Storefront.UI.Web.Views");
                    s.ConnectImplementationsToTypesClosing(typeof(IDomainEventHandler<>));
                });                
            }
        }                    
    }
}