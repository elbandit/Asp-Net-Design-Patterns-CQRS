using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Sales.Model.Offers
{
    public class OfferNotApplicableException : DomainException
    {
        public OfferNotApplicableException(string message_for_customer) : base(message_for_customer)
        {
        }
    }
}
