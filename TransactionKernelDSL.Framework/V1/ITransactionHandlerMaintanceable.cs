using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate bool DoMaintenanceDelegate();
    public delegate bool MaintenanceHandlerFactoryDelegate();

    public interface ITransactionHandlerMaintenanceable
    {
        bool DoMaintenance();
        bool MaintenanceHandlerFactory();
    }
}
