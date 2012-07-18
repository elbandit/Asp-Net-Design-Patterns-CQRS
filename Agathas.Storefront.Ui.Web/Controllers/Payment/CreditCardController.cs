using System.Web.Mvc;
using Agathas.Storefront.Application;
using Agathas.Storefront.Infrastructure.PaymentGateway.CreditCard;
using Agathas.Storefront.Sales.Api;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Ui.Web.Views;
using Agathas.Storefront.Ui.Web.Views.View;

namespace Agathas.Storefront.Ui.Web.Controllers.Payment
{
    public class CreditCardController : Controller
    {
        private readonly QueryService _queryService;
        private readonly ICreditCardPaymentGateway _credit_card_payment_gateway;
        private readonly Api _application;

        public CreditCardController(Api application, 
                                    QueryService query_service,
                                    ICreditCardPaymentGateway credit_card_payment_gateway)
        {            
            _queryService = query_service;
            _credit_card_payment_gateway = credit_card_payment_gateway;
            _application = application;
        }
             
        [HttpPost]
        public ActionResult CaptureFundsForOrder(long order_id, int card_details)
        {
            var order_details = _queryService.get_view_of<OrderDetailsForPayment>(x => x.order_id == order_id);

            var payment_details = new OrderDetailsRequiredForCreditCardPayment();

            payment_details.card_to_use = card_details;
            payment_details.amount_to_pay = order_details.amount_to_pay;
            payment_details.currency = order_details.currency;

            var result = _credit_card_payment_gateway.capture_funds(payment_details);

            // TODO: update MarkOrderHasFundsAquiredCommand with result

            _application.action_request_to(new MarkOrderHasFundsAquiredCommand());

            return RedirectToAction("CheckOut", "ThankYou");
        }
    }
}
