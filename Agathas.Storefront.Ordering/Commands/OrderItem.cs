using Agathas.Storefront.Common;

namespace Agathas.Storefront.Ordering.Commands
{
    public class OrderItem
    {
        public int product { get; set; }
        public int quantity { get; set; }
        public Money unit_price { get; set; }
    }
}