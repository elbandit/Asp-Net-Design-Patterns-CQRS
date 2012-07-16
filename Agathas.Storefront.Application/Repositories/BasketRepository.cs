using System;
using Agathas.Storefront.Sales.Model.Baskets;

namespace Agathas.Storefront.Sales.Api.Repositories
{
    public class BasketRepository : NhRepository<Basket, Guid>,  IBasketRepository
    {
        public BasketRepository(ISessionCoordinator session)
            : base(session)
        {
        }
    }
}