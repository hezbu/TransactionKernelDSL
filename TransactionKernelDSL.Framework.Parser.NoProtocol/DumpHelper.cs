using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionKernelDSL.Framework.Parser.NoProtocol
{
    internal static class DumpHelper
    {
        public static string Dump(byte[] src, int len, int offset = 0)
        {
            var result = String.Empty;
            for (int i = 0; i < len; i++)
            {
                result += String.Format("{0:X2} ", src[i + offset]);
            }

            return result.Trim();
        }
    }
}
