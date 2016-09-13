using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993.Tokens._R1_CORR_OPER_AUTH_DATA_TKN
{
    public sealed class _R1_CORR_OPER_AUTH_DATA_TKN : Iso8583_1993AbstractToken
    {
        private string _CORR_PAN = "                ";

        public string CORR_PAN
        {
            get
            {
                if (String.IsNullOrEmpty(_CORR_PAN)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNR1_CORR_PAN);
                return _CORR_PAN;
            }
            set
            {
                if (value.Length != 16) throw new ApplicationException(Properties.Resources.ErrorGet_TKNR1_CORR_PAN);
                _CORR_PAN = value;
            }
        }

        public _R1_CORR_OPER_AUTH_DATA_TKN()
        {
            _TokenId = "R1";
        }

        public _R1_CORR_OPER_AUTH_DATA_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(3)");
            if (assembledToken.Substring(10).Length != 16) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(6)");
        }

        public override bool AssembleToken()
        {
            _TokenData = CORR_PAN;
            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 16)
            {
                _ErrorMessage = "El payload para el token _R1_CORR_OPER_AUTH_DATA_TKN no es 16, es " + payload.Length;
            }
            else
            {
                try
                {
                    CORR_PAN = payload.Substring(0, 16);
                    retVal = true;
                }
                catch (ApplicationException appEx)
                {
                    _ErrorMessage = appEx.Message;

                }


            }

            return retVal;
        }

        public override string Dump()
        {
            return base.Dump() + "CORR_PAN: " + _CORR_PAN + " (" + _CORR_PAN.Length + ")\r\n";
        }
    }
}
