using System;

namespace Agathas.Storefront.Infrastructure.PaymentGateway.CreditCard
{
    public class OrderDetailsRequiredForCreditCardPayment
    {
        public int card_details { get; set; }

        public decimal amount_to_pay { get; set; }

        public string currency { get; set; }

        public int card_to_use { get; set; }
    }
}