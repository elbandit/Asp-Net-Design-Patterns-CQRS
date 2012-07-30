using System.Collections.Generic;

namespace Agathas.Storefront.Shipping.Model
{
    public class ShippingOptionsService
    {
        public IEnumerable<ShippingOption> get_shipping_options_for(List<ShippingItem> items, int country)
        {
            return new List<ShippingOption>();
        }
    }
}
