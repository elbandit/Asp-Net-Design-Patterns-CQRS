using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Ui.Web.Views.View
{
    public class BasketDetailView : IDomainView
    {
        public Guid id { get; set; }

        public string amount_to_pay { get; set; }

        public string message { get; set; }
    }
}