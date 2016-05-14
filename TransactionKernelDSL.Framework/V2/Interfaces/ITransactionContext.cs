using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionKernelDSL.Framework.V2.Interfaces
{
    public interface ITransactionContext : IEnumerable<KeyValuePair<string, string>>
    {
        string this[string keyNameParam] { get; set; }
    }
}
