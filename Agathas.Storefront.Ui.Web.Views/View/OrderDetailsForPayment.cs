using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Ui.Web.Views.View
{
    public class OrderDetailsForPayment : IDomainView
    {
        public long order_id { get; set; }

        public decimal amount_to_pay { get; set; }

        public string currency { get; set; }
    }
}
