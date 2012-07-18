using System.Web.Mvc;
using Agathas.Storefront.Application;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Ui.Web.Views;
using Agathas.Storefront.Ui.Web.Views.View;

namespace Agathas.Storefront.Ui.Web.Controllers.Payment
{
    public class PayPalController : Controller
    {
        private readonly QueryService _queryService;       
        private readonly Api _application;

        public PayPalController(Api application, 
                                QueryService query_service)
        {            
            _queryService = query_service;
            _application = application;
        }

        [HttpPost]
        public ActionResult PayForOrder(long order_id)
        {
            var order_details = _queryService.get_view_of<OrderDetailsForPayment>(x => x.order_id == order_id);

            return View(order_details); // We get the call back later and then update the model to show that money was taken
        }

        [HttpPost]
        public ActionResult PayPalNotifyCallBack(FormCollection collection)
        {
            // this is the call back from PayPal
            // var order_details = _queryService.get_view_of<OrderDetailsForPayment>(x => x.order_id == order_id);

            _application.action_request_to(new MarkOrderHasFundsAquiredCommand());

            return Json(new {confirmation = true});
        }
    }
}
