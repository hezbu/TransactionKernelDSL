using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens
{
    public sealed class Iso8583B24HeaderToken
    {
        public static char EyeCatcher = '&';

        private const string _EyeCatcher = "& ";
        private string _TokenCount;
        private string _TokenLength;

        private Iso8583B24HeaderToken()
        {

        }

        public Iso8583B24HeaderToken(List<Iso8583B24AbstractToken> tokens)
        {
            int totalLength = 0;
            int totalCount = 1;

            foreach (Iso8583B24AbstractToken token in tokens)
            {
                totalLength += token.ToString().Length;
                totalCount++;
            }

            _TokenCount = totalCount.ToString().PadLeft(5, '0');
            _TokenLength = (totalLength + _TokenCount.Length + _EyeCatcher.Length + 5).ToString().PadLeft(5, '0');
        }

        public Iso8583B24HeaderToken(string rawTokens)
        {
            if (rawTokens.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNHeader + " (0)");
            if (rawTokens.Length < 12) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNHeader + " (1)");

            _TokenCount = rawTokens.Substring(2, 5);
            _TokenLength = rawTokens.Substring(7, 5);
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(_TokenCount)) throw new ApplicationException(Properties.Resources.ErrorToString_TKNHeader);
            return _EyeCatcher + _TokenCount + _TokenLength;
        }
    }
}
