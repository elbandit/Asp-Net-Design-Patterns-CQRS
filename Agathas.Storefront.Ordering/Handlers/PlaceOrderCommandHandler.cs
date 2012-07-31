using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Ordering.Commands;

namespace Agathas.Storefront.Ordering.Handlers
{
    public class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand>
    {
        public void action(PlaceOrderCommand business_request)
        {
            throw new NotImplementedException();
        }
    }
}
