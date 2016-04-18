using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate bool AssembleDelegate();
    public delegate bool DisassembleDelegate();

    public interface ITransactionParserAssembleable
    {
        bool Assemble();
        bool Disassemble();
    }
}
