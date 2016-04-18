using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens._C0_PS51_TKN;
using TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens._04_PS50_TKN;
using TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens._C4_PT_SRV_DATA_TKN;
using TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens._U1_EBT_AVAIL_BAL_TKN;

namespace TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens
{
    public sealed class Iso8583B24TokenBuilder
    {
        #region Fields
        private Iso8583B24HeaderToken _TokenHeader = null;
        private List<Iso8583B24AbstractToken> _Tokens = null;
        #endregion
        #region Properties
        public List<Iso8583B24AbstractToken> Tokens
        {
            get
            {
                return _Tokens;
            }
        }

        public Iso8583B24HeaderToken TokenHeader
        {
            get
            {
                return _TokenHeader;
            }
        }
        #endregion


        #region Constructors
        public Iso8583B24TokenBuilder(string field63)
            : this()
        {
            _TokenHeader = new Iso8583B24HeaderToken(field63);
            Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Raw Header -> " + _TokenHeader.ToString());
            Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Raw Tokens -> " + field63.Substring(_TokenHeader.ToString().Length));
            foreach (string rawToken in field63.Substring(_TokenHeader.ToString().Length).Split(new char[] { Iso8583B24AbstractToken.EyeCatcher }))
            {
                if (String.IsNullOrEmpty(rawToken)) continue;
                string rawTokenWithEyeCatch = Iso8583B24AbstractToken.EyeCatcher + rawToken;

                Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Raw Token -> " + rawTokenWithEyeCatch);
                switch (rawTokenWithEyeCatch.Substring(2, 2))
                {
                    case "04":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token 04 ...");
                        _Tokens.Add(new _04_PS50_TKN._04_PS50_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token 04 added OK!");
                        break;
                    case "C0":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token C0 ...");
                        _Tokens.Add(new _C0_PS51_TKN._C0_PS51_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token C0 added OK!");
                        break;
                    case "R1":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token R1 ...");
                        _Tokens.Add(new _R1_CORR_OPER_AUTH_DATA_TKN._R1_CORR_OPER_AUTH_DATA_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token R1 added OK!");
                        break;
                    case "C4":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token C4 ...");
                        _Tokens.Add(new _C4_PT_SRV_DATA_TKN._C4_PT_SRV_DATA_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token C4 added OK!");
                        break;
                    case "Q1":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token Q1 ...");
                        _Tokens.Add(new _Q1_AUTH_MODE_ID_TKN._Q1_AUTH_MODE_ID_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token Q1 added OK!");
                        break;
                    case "Q6":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token Q6 ...");
                        _Tokens.Add(new _Q6_PART_CHGS_TKN._Q6_PART_CHGS_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token Q6 added OK!");
                        break;
                    case "Q2":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token Q2 ...");
                        _Tokens.Add(new _Q2_ACC_ID_TKN._Q2_ACC_ID_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token Q2 added OK!");
                        break;
                    case "U1":
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Adding Token U1 ...");
                        _Tokens.Add(new _U1_EBT_AVAIL_BAL_TKN._U1_EBT_AVAIL_BAL_TKN(rawTokenWithEyeCatch));
                        Debug.WriteLine("Iso8583B24TokenBuilder, ctor: Token U1 added OK!");
                        break;

                    default:
                        throw new ApplicationException("Token " + rawTokenWithEyeCatch.Substring(2, 2) + " not implemented");
                }
            }
        }

        private Iso8583B24TokenBuilder()
        {
            _Tokens = new List<Iso8583B24AbstractToken>();
        }

        public Iso8583B24TokenBuilder(List<Iso8583B24AbstractToken> tokenList)
        {
            _Tokens = tokenList;
            _TokenHeader = new Iso8583B24HeaderToken(_Tokens);
        }
        #endregion

        public void AddToken(Iso8583B24AbstractToken newToken)
        {
            _Tokens.Add(newToken);
        }

        public void RemoveToken(Iso8583B24AbstractToken tokenToRemove)
        {
            _Tokens.Remove(tokenToRemove);
        }

        public override string ToString()
        {
            string retValue;

            retValue = _TokenHeader.ToString();
            Debug.WriteLine("Iso8583B24TokenBuilder, ToString() -> H: " + _TokenHeader.ToString());
            foreach (Iso8583B24AbstractToken tok in _Tokens)
            {
                Debug.WriteLine("Iso8583B24TokenBuilder, ToString() -> T: " + tok.ToString());
                retValue += tok.ToString();
            }

            Debug.WriteLine("Iso8583B24TokenBuilder, ToString() -> " + retValue);

            return retValue;
        }

        public string Dump()
        {
            string retValue = String.Empty;


            foreach (Iso8583B24AbstractToken tok in _Tokens)
            {
                retValue += "-------------------------------------------\r\n";
                retValue += tok.Dump();
                retValue += "-------------------------------------------\r\n";
                retValue += "\r\n\r\n";

            }

            Debug.WriteLine("Iso8583B24TokenBuilder, Dump() -> " + retValue);

            return retValue;
        }

    }
}
