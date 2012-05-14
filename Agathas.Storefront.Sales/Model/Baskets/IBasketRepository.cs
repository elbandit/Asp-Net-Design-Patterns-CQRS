using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Sales.Model.Baskets
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
        //Basket find_by(int basketId);
        //void add(Basket basket);
    }
}