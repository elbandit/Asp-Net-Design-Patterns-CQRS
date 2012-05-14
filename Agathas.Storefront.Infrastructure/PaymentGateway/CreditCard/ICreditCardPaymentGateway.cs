namespace Agathas.Storefront.Infrastructure.PaymentGateway.CreditCard
{
    public interface ICreditCardPaymentGateway
    {
        PaymentResult capture_funds(OrderDetailsRequiredForCreditCardPayment order_details);
    }
}
