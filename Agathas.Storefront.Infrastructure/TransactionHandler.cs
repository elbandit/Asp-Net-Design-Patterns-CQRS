using System;

namespace Agathas.Storefront.Infrastructure
{
    public class TransactionHandler
    {
        private readonly IUnitOfWork _unitOfWork;        

        public TransactionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public void action<T>(T command, ICommandHandler<T> commandHandler) where T : IBusinessRequest
        {
            try
            {
                commandHandler.action(command);
                _unitOfWork.Commit();
            }
            catch (Exception Ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
