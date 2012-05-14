using System;

namespace Agathas.Storefront.Infrastructure
{
    public interface ICommandHandlerRegistry
    {
        Action<TCommand> find_handler_for<TCommand>(TCommand command) where TCommand : IBusinessRequest;
    }
}