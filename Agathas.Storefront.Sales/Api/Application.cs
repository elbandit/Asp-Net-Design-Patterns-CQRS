using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Sales.Api
{ 
    // API is a Facade into the application see DDD facade pattern
    public class Application
    {
        private ICommandHandlerRegistry _command_handler_registery;        

        public Application(ICommandHandlerRegistry command_handler_registery)
        {
            _command_handler_registery = command_handler_registery;
        }

        public void action_request_to<T>(T business_case) where T : IBusinessRequest
        {                        
            _command_handler_registery.find_handler_for(business_case).Invoke(business_case);
        }        
    }
}
