using Agathas.Storefront.Common;

namespace Agathas.Storefront.Shipping.Model
{
    public class ShippingOption 
    {
        public int id { get; set; }
        public string description { get; set; }
        public Money cost { get; set; }
    }
}