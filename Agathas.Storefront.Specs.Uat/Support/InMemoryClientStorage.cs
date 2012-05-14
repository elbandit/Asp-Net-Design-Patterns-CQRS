using System;
using System.Collections.Generic;
using Chap2.ShoppingBasket.Ui.Web.Controllers;

namespace Agathas.Storefront.Specs.Uat.Support
{
    public class InMemoryClientStorage : IClientStorage
    {
        private readonly IDictionary<string, Object> _dictionary;

        public InMemoryClientStorage()
        {
            _dictionary = new Dictionary<string, object>();
        }

        public bool contains_a_value_for_the_key(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public T get_value_for<T>(string key)
        {
            if (contains_a_value_for_the_key(key))
            {
                return (T)_dictionary[key];
            }
            else
            {
                return default(T);
            }
        }

        public void add<T>(string key, T value)
        {
            _dictionary.Add(key, value);
        }
    }
}
