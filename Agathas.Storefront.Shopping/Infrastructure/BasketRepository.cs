using System;
using Agathas.Storefront.Sales.Api.Repositories;
using Agathas.Storefront.Sales.Model.Baskets;
using Agathas.Storefront.Shopping.Baskets;

namespace Agathas.Storefront.Shopping.Infrastructure
{
    public class BasketRepository : NhRepository<Basket, Guid>,  IBasketRepository
    {
        public BasketRepository(ISessionCoordinator session)
            : base(session)
        {
        }
    }
}