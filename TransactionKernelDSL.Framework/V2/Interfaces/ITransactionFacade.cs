using System;
namespace TransactionKernelDSL.Framework.V1
{
    public interface ITransactionFacade
    {
        /// <summary>
        /// Gets the Instance Id assigned to the Transactional Facade
        /// </summary>
        int InstanceId { get; }              
        /// <summary>
        /// Starts every engine bound to the Transactional Facade
        /// </summary>
        /// <returns>True if operation was succesful, otherwise false</returns>
        bool StartEngines();
        /// <summary>
        /// Stops every engine bound to the Transactional Facade
        /// </summary>
        /// <returns>True if operation was succesful, otherwise false</returns>
        bool StopEngines();
    }
}
