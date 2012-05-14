using System.Web.Mvc;
using NUnit.Framework;

namespace Agathas.Storefront.Specs.Uat.Support
{
    public static class ActionResultExtensions
    {
        public static TModel Model<TModel>(this ActionResult result)
        {            
            Assert.IsInstanceOf(typeof(ViewResult), result);
            var viewResult = (ViewResult)result;
            Assert.IsNotNull(viewResult.ViewData.Model, "The action result does not contain a model");
            Assert.IsInstanceOf(typeof(TModel), viewResult.ViewData.Model, "The model in the action result is not of the right type");            
            return (TModel)viewResult.ViewData.Model;
        }
    }
}