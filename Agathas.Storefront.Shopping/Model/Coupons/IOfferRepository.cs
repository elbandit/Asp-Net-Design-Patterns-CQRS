using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Model.Coupons
{
    public interface IOfferRepository : IRepository<Offer, Guid>
    {
        Offer find_by(string voucher_code);
    }
}