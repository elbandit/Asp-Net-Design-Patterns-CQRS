using System;

namespace Agathas.Storefront.Infrastructure
{
    // If an exception occurs here then we can handle it.
    public class DomainException : ApplicationException 
    {
        public string message_for_customer { get; set; }

        public DomainException(string message_for_customer)
        {
            this.message_for_customer = message_for_customer;
        }
    }
}
