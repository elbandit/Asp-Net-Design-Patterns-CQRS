using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Sales.Model.Baskets.Events
{
    public class VoucherNotApplicableForBasket : IDomainEvent
    {       
        public VoucherNotApplicableForBasket(Guid basket_id, string reason)
        {
            this.basket_id = basket_id;
            this.reason = reason;
        }

        public string reason { get; set; }

        public Guid basket_id { get; set; }
    }
}
