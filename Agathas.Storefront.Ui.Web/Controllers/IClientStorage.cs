namespace Chap2.ShoppingBasket.Ui.Web.Controllers
{
    public interface IClientStorage
    {
        bool contains_a_value_for_the_key(string key);
        T get_value_for<T>(string key);
        void add<T>(string key, T value);
    }
}