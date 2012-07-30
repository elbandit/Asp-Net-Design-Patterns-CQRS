using Agathas.Storefront.Common;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Model.Baskets.Products;
using Agathas.Storefront.Shopping.Model.Promotions;
using Agathas.Storefront.Specs.Uat.Support;
using Agathas.Storefront.Ui.Web.Controllers;
using Agathas.Storefront.Ui.Web.Views.View;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;

namespace Agathas.Storefront.Specs.Uat.Steps
{
    [Binding]
    public class BasketStepDefinitions
    {
        private IPromotionsRepository _promotions_repository;        
        private IProductRepository _productRepository;       
        private BasketController _basket_Controller;
        private IUnitOfWork _uow;
        

        public BasketStepDefinitions()
        {
            _promotions_repository = ObjectFactory.GetInstance<IPromotionsRepository>();
            _productRepository = ObjectFactory.GetInstance<IProductRepository>();
            _uow = ObjectFactory.GetInstance<IUnitOfWork>();

            _basket_Controller = ObjectFactory.GetInstance<BasketController>();            
        }


        [Given(@"the following offer")]
        public void GivenTheFollowingOffer(Table table)
        {
            foreach (var row in table.Rows)
            {
                _promotions_repository.add(new Promotion(row["VoucherCode"], new Money(decimal.Parse(row["Discount"])), new Money(decimal.Parse(row["Threshold"]))));   
            }

            _uow.Commit();
        }

        [Given(@"the following products")]
        public void GivenTheFollowingProducts(Table table)
        {
            foreach (var row in table.Rows)
            {
                _productRepository.add(new ProductSnapshot(int.Parse(row["ProductId"]), row["Name"], new Money(decimal.Parse(row["Price"])), row["Category"]));
            }

            _uow.Commit();
        }        

        [Given(@"I have added the following items to my basket")]
        public void GivenIHaveAddedTheFollowingItemsToMyBasket(Table table)
        {                                    
            foreach(var row in table.Rows)
            {
                _basket_Controller.Add(int.Parse(row["ProductId"]));
            }
        }

        [Then(@"the total amount payable on the basket should be £(.*)")]
        public void ThenTheTotalAmountPayableOnTheBasketShouldBe(decimal price)
        {
            var action_result = _basket_Controller.Index();

            Assert.AreEqual(new Money(price).ToString(), action_result.Model<BasketDetailView>().amount_to_pay);            
        }
           
        [When(@"I apply the promotional voucher")]
        public void WhenIApplyThePromotionalVoucher(Table table)
        {
             foreach(var row in table.Rows)
             {
                 _basket_Controller.ApplyVoucher(row["VoucherCode"]);
             }            
        }

        [When(@"I remove product id '(.*)'")]
        public void WhenIRemoveProductId(int product_id)
        {
            _basket_Controller.Remove(product_id);
        }

        [Then(@"I should be told that ""(.*)""")]
        public void ThenIShouldBeToldThat(string message)
        {
            var action_result = _basket_Controller.Index();

            Assert.AreEqual(message, action_result.Model<BasketDetailView>().message);    
        }

        [When(@"I remove the promotional voucher")]
        public void WhenIRemoveThePromotionalVoucher(Table table)
        {
            foreach (var row in table.Rows)
            {
                _basket_Controller.RemoveVoucher(row["VoucherCode"]);
            } 
        }
    }
}
