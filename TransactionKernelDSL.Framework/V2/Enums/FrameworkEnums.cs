using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionKernelDSL.Framework.V2.Enums
{
    public enum TransactionStatus
    {
        TransactionNotProcessed = 0,
        TransactionApproved = 1,
        TransactionRejectedByListener = 2,
        TransactionUnknownState = 3,
        TransactionRejectedByForwarder = 4,
        TransactionPending = 5,
        TransactionReversed = 6,
        UnableToConnectForwarderByListener = 7,
        UnableToConnectForwarderByForwarder = 8,
        ForwarderTimeoutByForwarder = 9,
        RefundRejectedByApprovedTransaction = 10,
        RefundRejectedByInexistentTransaction = 11

    }

    [Flags]
    public enum TransmissionStatus
    {
        NoError = 0x00000000,
        ContactLost = 0x00000001,
        ProblemDuringContact = 0x00000002,
        Timeout = 0x00000004,
        BadAssembling = 0x00000008,
        BadDisassembling = 0x00000010,
        SendingError = 0x00000020,
        HeaderNotFound = 0x00000040,
        SocketError = 0x00000080,
        ConnectionError = 0x00000100,
        DisconnectionError = 0x00000200,


        SessionError = 0x00001000
    }

    [Flags]
    public enum DebugTransactionStatus
    {
        NothingHappenned = 0x00000000,
        InitializedOk = 0x00000001,
        ReceivedOk = 0x00000002,
        DisassembledOk = 0x00000004,
        AssembledOk = 0x00000008,
        SentOk = 0x00000010,
        GeneralExceptionFound = 0x00000020,
        GeneralContextFactoringOk = 0x00000040,
        TransactionContextFactoringOk = 0x00000080,
        PreProcessingOk = 0x00000100,
        ForwarderSideFactoringOk = 0x00000200,
        DoingProcessTemplateOk = 0x00000400,
        RegisteringTransactionOk = 0x00000800,
        BuildingResponseOk = 0x00001000
    }
}
