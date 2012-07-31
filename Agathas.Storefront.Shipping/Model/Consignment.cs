using System.Collections.Generic;

namespace Agathas.Storefront.Shipping.Model
{
    public class Consignment
    {
        public bool is_shippable { get; set; }
        public List<Item> items { get; set; }
    }
}