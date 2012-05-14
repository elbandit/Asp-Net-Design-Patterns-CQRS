using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Ui.Web.Views
{ 
    // API is a Facade into the application see DDD facade pattern
    public class QueryService
    {        
        private readonly IQueryService _query_service;

        public QueryService(IQueryService query_service)
        {     
            _query_service = query_service;
        }
                
        public T get_view_of<T>(Func<T, bool> query) where T : IDomainView
        {
            return _query_service.query_for_single(query);
        }
    }
}
