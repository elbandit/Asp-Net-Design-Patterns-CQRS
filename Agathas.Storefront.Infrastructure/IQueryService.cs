using System;

namespace Agathas.Storefront.Infrastructure
{
    public interface IQueryService
    {
        T query_for_single<T>(Func<T, bool> query);
        T query_for_single<T>(object id);
    }
}
