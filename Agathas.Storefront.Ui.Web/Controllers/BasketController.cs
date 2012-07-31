using System;
using System.Web.Mvc;
using Agathas.Storefront.Application;
using Agathas.Storefront.Application.Commands;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Sales.Api;
using Agathas.Storefront.Sales.Api.Commands;
using Agathas.Storefront.Shopping.Commands;
using Agathas.Storefront.Ui.Web.Views;
using Agathas.Storefront.Ui.Web.Views.View;
using Chap2.ShoppingBasket.Ui.Web.Controllers;

namespace Agathas.Storefront.Ui.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly Api _application;
        private readonly QueryService _query_service;
        private readonly IClientStorage _client_storage;
        
        public BasketController(Api application, QueryService query_service, IClientStorage client_storage)
        {
            _application = application;
            _query_service = query_service;
            _client_storage = client_storage;
        }

        public ActionResult Index()
        {
            Guid basket_id = get_basket_id();

            var basket_detail_view = _query_service.get_view_of<BasketDetailView>(x => x.id == basket_id);

            return View(basket_detail_view);            
        }

        [HttpPost]
        public ActionResult ApplyVoucher(string voucher_code)
        {            
            var apply_coupon_to_basket_command = new ApplyCouponToBasketCommand(get_basket_id(), voucher_code);

            handle_domain_exception(() =>
                                        {
                                            _application.action_request_to(apply_coupon_to_basket_command);
                                        });
            
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult Add(int productid)
        {
            Guid basket_id = get_basket_id();

            // TODO: Query system to see if a product with the id exists

            var add_product_to_basket = new AddProductToBasketCommand(productid, basket_id);
            _application.action_request_to(add_product_to_basket);

            return Redirect("Index");
        }

        private Guid get_basket_id()
        {
            Guid basket_id;            
            if (!_client_storage.contains_a_value_for_the_key("basket_id"))
            {
                basket_id = Guid.NewGuid();
                _client_storage.add("basket_id", basket_id);

                var create_basket = new CreateABasketCommand(basket_id);
                _application.action_request_to(create_basket);
            }
            else
            {
                basket_id = _client_storage.get_value_for<Guid>("basket_id");
            }
            return basket_id;
        }

        [HttpPost]
        public ActionResult Remove(int product_id)
        {
            var remove_product_from_basket = new RemoveProductFromBasketCommand(get_basket_id(), product_id);
            _application.action_request_to(remove_product_from_basket);

            return Redirect("Index");  
        }

        public void handle_domain_exception(System.Action action)
        {
            try
            {
                action();
            }
            catch (DomainException Ex)
            {
                display_to_user(Ex);                
            }
        }

        private void display_to_user(DomainException domain_exception)
        {
            // Set MVC Temp data with what when wrong
        }

        [HttpPost]
        public ActionResult RemoveVoucher(string voucher_code)
        {
            var remove_voucher_from_basket = new RemoveCouponFromBasketCommand(get_basket_id(), voucher_code);

            handle_domain_exception(() =>
            {
                _application.action_request_to(remove_voucher_from_basket);
            });

            return Redirect("Index");  
        }
    }

}
