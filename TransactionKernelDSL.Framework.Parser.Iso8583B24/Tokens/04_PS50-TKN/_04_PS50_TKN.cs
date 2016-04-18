using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens._04_PS50_TKN
{
    public sealed class _04_PS50_TKN : Iso8583B24AbstractToken
    {
        private string _ERR_FLG = " ";
        private string _RTE_GRP;
        private string _CRD_VRFY_FLG = " ";
        private string _CITY_EXT = "     ";
        private string _COMPLETE_TRACK2_DATA;
        private string _UAF_FLG = " ";

        #region Properties
        /// <summary>
        /// Credit Card Authentication Additional Info
        /// </summary>
        public string ERR_FLG
        {
            get
            {
                if (String.IsNullOrEmpty(_ERR_FLG)) throw new ApplicationException(Properties.Resources.ErrorGet_TKN04_ERR_FLG);
                return _ERR_FLG;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKN04_ERR_FLG);
                _ERR_FLG = value;
            }
        }
        /// <summary>
        /// Routing Group for Acquiring Routing
        /// </summary>
        public string RTE_GRP
        {
            get
            {
                if (String.IsNullOrEmpty(_RTE_GRP)) throw new ApplicationException(Properties.Resources.ErrorGet_TKN04_RTE_GRP);
                return _RTE_GRP.PadLeft(11, '0');
            }
            set
            {
                if (value.Length > 11) throw new ApplicationException(Properties.Resources.ErrorSet_TKN04_RTE_GRP);
                _RTE_GRP = value;
            }
        }
        /// <summary>
        /// It indicates whether CVC has been authorized by acquiring
        /// </summary>
        public string CRD_VRFY_FLG
        {
            get
            {
                if (String.IsNullOrEmpty(_CRD_VRFY_FLG)) throw new ApplicationException(Properties.Resources.ErrorGet_TKN04_CRD_VRFY_FLG);
                return _CRD_VRFY_FLG;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKN04_CRD_VRFY_FLG);
                _CRD_VRFY_FLG = value;
            }
        }
        /// <summary>
        /// It indicates whether CVC has been authorized by acquiring
        /// </summary>
        public string CITY_EXT
        {
            get
            {
                if (String.IsNullOrEmpty(_CITY_EXT)) throw new ApplicationException(Properties.Resources.ErrorGet_TKN04_CITY_EXT);
                return _CITY_EXT;
            }
            set
            {
                if (value.Length > 5) throw new ApplicationException(Properties.Resources.ErrorSet_TKN04_CITY_EXT);
                _CITY_EXT = value;
            }
        }
        /// <summary>
        /// It indicated whether terminal or acquiring is capable of transmitting complete Track2 or track2
        /// </summary>
        public string COMPLETE_TRACK2_DATA
        {
            get
            {
                if (String.IsNullOrEmpty(_COMPLETE_TRACK2_DATA)) throw new ApplicationException(Properties.Resources.ErrorGet_TKN04_COMPLETE_TRACK2_DATA);
                return _COMPLETE_TRACK2_DATA;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKN04_COMPLETE_TRACK2_DATA);
                _COMPLETE_TRACK2_DATA = value;
            }
        }
        /// <summary>
        /// It indicated whether card includes registers stored in UAF file
        /// </summary>
        public string UAF_FLG
        {
            get
            {
                if (String.IsNullOrEmpty(_UAF_FLG)) throw new ApplicationException(Properties.Resources.ErrorGet_TKN04_UAF_FLG);
                return _UAF_FLG;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKN04_UAF_FLG);
                _UAF_FLG = value;
            }
        }
        #endregion

        public _04_PS50_TKN()
        {
            _TokenId = "04";
        }

        public _04_PS50_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKN04 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKN04 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKN04 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKN04 + "(3)");
            if (assembledToken.Substring(10).Length != 20) throw new ApplicationException(Properties.Resources.ErrorCtor_TKN04 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKN04 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKN04 + "(6)");
        }

        public override bool AssembleToken()
        {
            _TokenData = ERR_FLG + RTE_GRP + CRD_VRFY_FLG + CITY_EXT + COMPLETE_TRACK2_DATA + UAF_FLG;
            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 20)
            {
                _ErrorMessage = "El payload para el token 04_PS50_TKN no es 20, es " + payload.Length;
            }
            else
            {
                try
                {
                    ERR_FLG = payload.Substring(0, 1);
                    RTE_GRP = payload.Substring(0 + 1, 11);
                    CRD_VRFY_FLG = payload.Substring(0 + 1 + 11, 1);
                    CITY_EXT = payload.Substring(0 + 1 + 11 + 1, 5);
                    COMPLETE_TRACK2_DATA = payload.Substring(0 + 1 + 11 + 1 + 5, 1);
                    UAF_FLG = payload.Substring(0 + 1 + 11 + 1 + 5 + 1, 1);

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
            return base.Dump() + "ERR_FLG: " + ERR_FLG + " (" + ERR_FLG.Length + ")\r\n"
                                + "RTE_GRP: " + RTE_GRP + " (" + RTE_GRP.Length + ")\r\n"
                                + "CRD_VRFY_FLG: " + CRD_VRFY_FLG + " (" + CRD_VRFY_FLG.Length + ")\r\n"
                                + "CITY_EXT: " + CITY_EXT + " (" + CITY_EXT.Length + ")\r\n"
                                + "COMPLETE_TRACK2_DATA: " + COMPLETE_TRACK2_DATA + " (" + COMPLETE_TRACK2_DATA.Length + ")\r\n"
                                + "UAF_FLG: " + UAF_FLG + " (" + UAF_FLG.Length + ")\r\n";

        }



    }
}
