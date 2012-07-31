using System.Collections.Generic;
using Agathas.Storefront.Common;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Ordering.Commands
{
    public class PlaceOrderCommand : IBusinessRequest
    {
        // Shipping Option
        public int shipping_option_id { get; set; }
        public Money shipping_option_cost { get; set; }

        // Items
        public List<OrderItem> items { get; set; }

        // Delivery Address
        public Address address { get; set; }
    
    }
}