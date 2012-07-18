﻿using System;
using Agathas.Storefront.Promotions.Coupons;
using Agathas.Storefront.Sales.Api.Repositories;
using Agathas.Storefront.Sales.Model.Offers;

namespace Agathas.Storefront.Promotions.Infrastructure
{
    public class OfferRepository : NhRepository<Offer, Guid>, IOfferRepository
    {
        public OfferRepository(ISessionCoordinator session)
            : base(session)
        {
        }
        
        public Offer find_by(string voucher_code)
        {
            return base.query_for_single(x => x.voucher_code == voucher_code);
        }
    }
}