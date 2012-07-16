using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Sales.Api.Commands
{
    public class RemoveOfferFromBasketCommand : IBusinessRequest
    {
        private readonly string _offer_code;
        private readonly Guid _basket_id;

        public RemoveOfferFromBasketCommand(Guid basket_id, string offer_code)
        {
            _offer_code = offer_code;
            _basket_id = basket_id;
        }

        public string offer_code
        {
            get { return _offer_code; }
        }

        public Guid basket_id
        {
            get { return _basket_id; }
        }
    }
}