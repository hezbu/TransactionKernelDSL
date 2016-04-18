using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate bool SendDelegate(object handler);
    public delegate bool ReceiveDelegate(object handler);
    public delegate bool IsKeepAliveMessageDelegate();

    public interface ITransactionParserCommunicable
    {
        bool Send(object handler);
        bool Receive(object handler);
        bool IsKeepAliveMessage();
    }
}
