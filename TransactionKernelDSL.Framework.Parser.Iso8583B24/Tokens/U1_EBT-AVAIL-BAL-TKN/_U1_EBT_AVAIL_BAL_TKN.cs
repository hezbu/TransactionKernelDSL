using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens._U1_EBT_AVAIL_BAL_TKN
{
    public sealed class _U1_EBT_AVAIL_BAL_TKN : Iso8583B24AbstractToken
    {
        private string _CASH_ACCT_BAL_IND = "0";
        private string _CASH_ACCT_BAL = "000000000000000000";
        private string _FOOD_STMP_BAL_IND = "0";
        private string _FOOD_STMP_BAL = "000000000000000000";

        #region Properties
        /// <summary>
        /// A flag indicating whether a balance is present for a cash benefit account. Valid values are as follows:
        /// 0 = No, the balance is not available.
        /// 1 = Yes, the balance present.
        /// </summary>
        public string CASH_ACCT_BAL_IND
        {
            get
            {
                if (String.IsNullOrEmpty(_CASH_ACCT_BAL_IND)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNU1_CASH_ACCT_BAL_IND);
                return _CASH_ACCT_BAL_IND;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNU1_CASH_ACCT_BAL_IND);
                _CASH_ACCT_BAL_IND = value;
            }
        }
        /// <summary>
        /// The available balance for the cash benefit account.
        /// </summary>
        public string CASH_ACCT_BAL
        {
            get
            {
                if (String.IsNullOrEmpty(_CASH_ACCT_BAL)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNU1_CASH_ACCT_BAL);
                return _CASH_ACCT_BAL;
            }
            set
            {
                if (value.Length > 18) throw new ApplicationException(Properties.Resources.ErrorSet_TKNU1_CASH_ACCT_BAL);
                _CASH_ACCT_BAL = value.PadLeft(18, '0');
            }
        }
        /// <summary>
        /// A flag indicating whether a balance is present for a food stamp
        /// account. Valid values are as follows:
        /// 0 = No, the balance is not available.
        /// 1 = Yes, the balance present.
        /// </summary>
        public string FOOD_STMP_BAL_IND
        {
            get
            {
                if (String.IsNullOrEmpty(_FOOD_STMP_BAL_IND)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNU1_FOOD_STMP_BAL_IND);
                return _FOOD_STMP_BAL_IND;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNU1_FOOD_STMP_BAL_IND);
                _FOOD_STMP_BAL_IND = value;
            }
        }
        /// <summary>
        /// The available balance for the food stamp account.
        /// </summary>
        public string FOOD_STMP_BAL
        {
            get
            {
                if (String.IsNullOrEmpty(_FOOD_STMP_BAL)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNU1_FOOD_STMP_BAL);
                return _FOOD_STMP_BAL;
            }
            set
            {
                if (value.Length > 18) throw new ApplicationException(Properties.Resources.ErrorSet_TKNU1_FOOD_STMP_BAL);
                _FOOD_STMP_BAL = value.PadLeft(18, '0');
            }
        }
        #endregion

        public _U1_EBT_AVAIL_BAL_TKN()
        {
            _TokenId = "U1";
        }

        public _U1_EBT_AVAIL_BAL_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNU1 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNU1 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNU1 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNU1 + "(3)");
            if (assembledToken.Substring(10).Length != 38) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNU1 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNU1 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNU1 + "(6)");
        }

        public override bool AssembleToken()
        {
            _TokenData = CASH_ACCT_BAL_IND + CASH_ACCT_BAL + FOOD_STMP_BAL_IND + FOOD_STMP_BAL;

            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 38)
            {
                _ErrorMessage = "El payload para el token U1_EBT_AVAIL_BAL_TKN no es 38, es " + payload.Length;
            }
            else
            {
                try
                {
                    CASH_ACCT_BAL_IND = payload.Substring(0, 1);
                    CASH_ACCT_BAL = payload.Substring(0 + 1, 18);
                    FOOD_STMP_BAL_IND = payload.Substring(0 + 1 + 18, 1);
                    FOOD_STMP_BAL = payload.Substring(0 + 1 + 18 + 1, 18);

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
            return base.Dump() + "CASH_ACCT_BAL_IND: " + CASH_ACCT_BAL_IND + " (" + CASH_ACCT_BAL_IND.Length + ")\r\n"
                                + "CASH_ACCT_BAL: " + CASH_ACCT_BAL + " (" + CASH_ACCT_BAL.Length + ")\r\n"
                                + "FOOD_STMP_BAL_IND: " + FOOD_STMP_BAL_IND + " (" + FOOD_STMP_BAL_IND.Length + ")\r\n"
                                + "FOOD_STMP_BAL: " + FOOD_STMP_BAL + " (" + FOOD_STMP_BAL.Length + ")\r\n"
                                ;

        }
    }
}
