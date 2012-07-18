using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Model.Coupons
{
    public class OfferNotApplicableException : DomainException
    {
        public OfferNotApplicableException(string message_for_customer) : base(message_for_customer)
        {
        }
    }
}
