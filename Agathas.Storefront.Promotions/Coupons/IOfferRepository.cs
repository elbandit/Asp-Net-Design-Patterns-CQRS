using System;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Promotions.Coupons;

namespace Agathas.Storefront.Sales.Model.Offers
{
    public interface IOfferRepository : IRepository<Offer, Guid>
    {
        Offer find_by(string voucher_code);
    }
}