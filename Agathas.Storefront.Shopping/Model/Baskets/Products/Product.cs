using Agathas.Storefront.Common;

namespace Agathas.Storefront.Shopping.Model.Baskets.Products          
{
    public class Product 
    {
        private readonly int _id;
        private string _name;
        private Money _price;
        private readonly string _category;

        private Product()
        {
        }

        public Product(int id, string name, Money price, string category)
        {
            // TODO: Check for null values and invalid data
            _id = id;
            _name = name;
            _price = price;
            _category = category;
        }

        public string name
        {
            get { return _name; }
        }

        public Money price
        {
            get { return _price; }
        }

        public int id
        {
            get { return _id; }
        }
    }
}