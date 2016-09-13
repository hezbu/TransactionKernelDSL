using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993.Tokens._Q1_AUTH_MODE_ID_TKN
{
    public sealed class _Q1_AUTH_MODE_ID_TKN : Iso8583_1993AbstractToken
    {
        private AUTH_MODE_ID_Values _AUTH_MODE_ID = AUTH_MODE_ID_Values.UnknownSituation;
        private string _USER_FLD2 = " ";

        /// <summary>
        /// Identificador del modo de autorización (4,5 o 9)
        /// Values
        /// 4 Autorizado off-line por el negocio, archivo negativo
        /// 5 Transacción forzada o de ajuste, 0220/0221
        /// 9 Situación desconocida.
        /// </summary>
        public string AUTH_MODE_ID
        {
            get { return _AUTH_MODE_ID.ToString("D"); }
            set 
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNQ1_AUTH_MODE_ID);
                _AUTH_MODE_ID = (AUTH_MODE_ID_Values)Enum.Parse(typeof(AUTH_MODE_ID_Values), value);                 
            }
        }
        public string USER_FLD2
        {
            get { return _USER_FLD2; }
        }

        public _Q1_AUTH_MODE_ID_TKN()
        {
            _TokenId = "Q1";
        }

        public _Q1_AUTH_MODE_ID_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(3)");
            if (assembledToken.Substring(10).Length != 2) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ1 + "(6)");
        }

        public override bool AssembleToken()
        {
            _TokenData = AUTH_MODE_ID + USER_FLD2;
            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 2)
            {
                _ErrorMessage = "El payload para el token _Q1_AUTH_MODE_ID_TKN no es 2, es " + payload.Length;
            }
            else
            {
                try
                {
                    AUTH_MODE_ID = payload.Substring(0, 1);
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
            return base.Dump() + "AUTH_MODE_ID: " + AUTH_MODE_ID + " (" + AUTH_MODE_ID.Length + ") -> "+_AUTH_MODE_ID+"\r\n"
                               + "USER_FLD2: " + USER_FLD2 + " (" + USER_FLD2.Length + ")\r\n";
        }

        public enum AUTH_MODE_ID_Values
        {
            /// <summary>
            /// Autorizado off-line por el negocio, archivo negativo
            /// </summary>
            OffLineApprovedByMerch_NegFile = 4,
            /// <summary>
            /// Transacción forzada o de ajuste, 0220/0221
            /// </summary>
            AdjustmentTransaction = 5,
            /// <summary>
            /// Situación desconocida.
            /// </summary>
            UnknownSituation = 9
        }
    }
}
