using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993.Tokens._C0_PS51_TKN
{
    public sealed class _C0_PS51_TKN : Iso8583_1993AbstractToken
    {
        private string _CVD_FLD = "    ";
        private string _RESUB_STAT = " ";
        private string _RESUB_CNTR = "   ";
        private string _TERM_POSTAL_CDE = null;
        private string _E_COM_FLG_MOTO_FLG = " ";
        private string _CMRCL_CRD_TYP = " ";
        private string _ADNL_DATA_IND = " ";
        private string _CVD_FLD_PRESENT = " ";
        private string _SAF_OR_FORCE_POST = " ";
        private string _AUTHN_COLL_IND = " ";
        private string _FRD_PRN_FLG = " ";
        private string _CAVV_AAV_RSLT_CDE = " ";

        #region Properties
        /// <summary>
        /// CVC2- Manual
        /// </summary>
        public string CVD_FLD
        {
            get
            {
                if (String.IsNullOrEmpty(_CVD_FLD)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_CVD_FLD);
                return _CVD_FLD;
            }
            set
            {
                if (value.Length > 4) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_CVD_FLD);
                _CVD_FLD = value;
            }
        }
        /// <summary>
        /// It indicates submitted transaction status 
        /// Values: Non applicable ( Visa )
        /// </summary>
        public string RESUB_STAT
        {
            get
            {
                if (String.IsNullOrEmpty(_RESUB_STAT)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_RESUB_STAT);
                return _RESUB_STAT;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_RESUB_STAT);
                _RESUB_STAT = value;
            }
        }
        /// <summary>
        /// Resending Attempts Counter – No applicable ( Visa )
        /// </summary>
        public string RESUB_CNTR
        {
            get
            {
                if (String.IsNullOrEmpty(_RESUB_CNTR)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_RESUB_CNTR);
                return _RESUB_CNTR;
            }
            set
            {
                if (value.Length > 3) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_RESUB_CNTR);
                _RESUB_CNTR = value;
            }
        }
        /// <summary>
        /// Terminal ZIP/ Post Code
        /// </summary>
        public string TERM_POSTAL_CDE
        {
            get
            {
                if (String.IsNullOrEmpty(_TERM_POSTAL_CDE)) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_RESUB_CNTR);
                return _TERM_POSTAL_CDE;
            }
            set
            {
                if (value.Length > 10) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_TERM_POSTAL_CDE);
                _TERM_POSTAL_CDE = value.PadRight(10, ' ');
            }
        }
        /// <summary>
        /// E-Comerce Flag.
        /// </summary>
        public string E_COM_FLG_MOTO_FLG
        {
            get
            {
                if (String.IsNullOrEmpty(_E_COM_FLG_MOTO_FLG)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_E_COM_FLG);
                return _E_COM_FLG_MOTO_FLG;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_E_COM_FLG);
                _E_COM_FLG_MOTO_FLG = value;
            }
        }
        /// <summary>
        /// Credit Card Type: Non applicable ( Visa )
        /// </summary>
        public string CMRCL_CRD_TYP
        {
            get
            {
                if (String.IsNullOrEmpty(_CMRCL_CRD_TYP)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_CMRCL_CRD_TYP);
                return _CMRCL_CRD_TYP;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_CMRCL_CRD_TYP);
                _CMRCL_CRD_TYP = value;
            }
        }
        /// <summary>
        /// Flag that indicates whether transaction includes original info. 
        /// </summary>
        public string ADNL_DATA_IND
        {
            get
            {
                if (String.IsNullOrEmpty(_ADNL_DATA_IND)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_ADNL_DATA_IND);
                return _ADNL_DATA_IND;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_ADNL_DATA_IND);
                _ADNL_DATA_IND = value;
            }
        }
        /// <summary>
        /// It indicates CVC2 presence or absence during transaction
        /// </summary>
        public string CVD_FLD_PRESENT
        {
            get
            {
                if (String.IsNullOrEmpty(_CVD_FLD_PRESENT)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_CVD_FLD_PRESENT);
                return _CVD_FLD_PRESENT;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_CVD_FLD_PRESENT);
                _CVD_FLD_PRESENT = value;
            }
        }
        /// <summary>
        /// It indicates transaction type for 0220 messages. 
        /// </summary>
        public string SAF_OR_FORCE_POST
        {
            get
            {
                if (String.IsNullOrEmpty(_SAF_OR_FORCE_POST)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_SAF_OR_FORCE_POST);
                return _SAF_OR_FORCE_POST;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_SAF_OR_FORCE_POST);
                _SAF_OR_FORCE_POST = value;
            }
        }
        /// <summary>
        /// UCAF Identifier status.
        /// </summary>
        public string AUTHN_COLL_IND
        {
            get
            {
                if (String.IsNullOrEmpty(_AUTHN_COLL_IND)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_AUTHN_COLL_IND);
                return _AUTHN_COLL_IND;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_AUTHN_COLL_IND);
                _AUTHN_COLL_IND = value;
            }
        }
        /// <summary>
        /// Fraud tendency Flag. Not applicable
        /// </summary>
        public string FRD_PRN_FLG
        {
            get
            {
                if (String.IsNullOrEmpty(_FRD_PRN_FLG)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_FRD_PRN_FLG);
                return _FRD_PRN_FLG;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_FRD_PRN_FLG);
                _FRD_PRN_FLG = value;
            }
        }
        /// <summary>
        /// AVV Verification Outcome. Not aplicable.
        /// </summary>
        public string CAVV_AAV_RSLT_CDE
        {
            get
            {
                if (String.IsNullOrEmpty(_CAVV_AAV_RSLT_CDE)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNC0_CAVV_AAV_RSLT_CDE);
                return _CAVV_AAV_RSLT_CDE;
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC0_CAVV_AAV_RSLT_CDE);
                _CAVV_AAV_RSLT_CDE = value;
            }
        }
        #endregion


        public _C0_PS51_TKN()
        {
            _TokenId = "C0";
        }

        public _C0_PS51_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC0 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC0 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC0 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC0 + "(3)");
            if (assembledToken.Substring(10).Length != 26) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC0 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC0 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC0 + "(6)");
        }

        public override bool AssembleToken()
        {
            _TokenData = CVD_FLD + RESUB_STAT + RESUB_CNTR + TERM_POSTAL_CDE +
                        E_COM_FLG_MOTO_FLG + CMRCL_CRD_TYP + ADNL_DATA_IND +
                        CVD_FLD_PRESENT + SAF_OR_FORCE_POST + AUTHN_COLL_IND +
                        FRD_PRN_FLG + CAVV_AAV_RSLT_CDE;

            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 26)
            {
                _ErrorMessage = "El payload para el token C0_PS51_TKN no es 26, es " + payload.Length;
            }
            else
            {
                try
                {
                    CVD_FLD = payload.Substring(0, 4);
                    RESUB_STAT = payload.Substring(0 + 4, 1);
                    RESUB_CNTR = payload.Substring(0 + 4 + 1, 3);
                    TERM_POSTAL_CDE = payload.Substring(0 + 4 + 1 + 3, 10);
                    E_COM_FLG_MOTO_FLG = payload.Substring(0 + 4 + 3 + 1 + 10, 1);

                    CMRCL_CRD_TYP = payload.Substring(0 + 4 + 3 + 1 + 10 + 1, 1);
                    ADNL_DATA_IND = payload.Substring(0 + 4 + 3 + 1 + 10 + 1 + 1, 1);
                    CVD_FLD_PRESENT = payload.Substring(0 + 4 + 3 + 1 + 10 + 1 + 1 + 1, 1);
                    SAF_OR_FORCE_POST = payload.Substring(0 + 4 + 3 + 1 + 10 + 1 + 1 + 1 + 1, 1);
                    AUTHN_COLL_IND = payload.Substring(0 + 4 + 3 + 1 + 10 + 1 + 1 + 1 + 1 + 1, 1);
                    FRD_PRN_FLG = payload.Substring(0 + 4 + 3 + 1 + 10 + 1 + 1 + 1 + 1 + 1 + 1, 1);
                    CAVV_AAV_RSLT_CDE = payload.Substring(0 + 4 + 3 + 1 + 10 + 1 + 1 + 1 + 1 + 1 + 1 + 1, 1);

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
            return base.Dump() + "CVD_FLD: " + CVD_FLD + " (" + CVD_FLD.Length + ")\r\n"
                                + "RESUB_STAT: " + RESUB_STAT + " (" + RESUB_STAT.Length + ")\r\n"
                                + "RESUB_CNTR: " + RESUB_CNTR + " (" + RESUB_CNTR.Length + ")\r\n"
                                + "TERM_POSTAL_CDE: " + TERM_POSTAL_CDE + " (" + TERM_POSTAL_CDE.Length + ")\r\n"
                                + "E_COM_FLG_MOTO_FLG: " + E_COM_FLG_MOTO_FLG + " (" + E_COM_FLG_MOTO_FLG.Length + ")\r\n"
                                + "CMRCL_CRD_TYP: " + CMRCL_CRD_TYP + " (" + CMRCL_CRD_TYP.Length + ")\r\n"
                                + "ADNL_DATA_IND: " + ADNL_DATA_IND + " (" + ADNL_DATA_IND.Length + ")\r\n"
                                + "CVD_FLD_PRESENT: " + CVD_FLD_PRESENT + " (" + CVD_FLD_PRESENT.Length + ")\r\n"
                                + "SAF_OR_FORCE_POST: " + SAF_OR_FORCE_POST + " (" + SAF_OR_FORCE_POST.Length + ")\r\n"
                                + "AUTHN_COLL_IND: " + AUTHN_COLL_IND + " (" + AUTHN_COLL_IND.Length + ")\r\n"
                                + "FRD_PRN_FLG: " + FRD_PRN_FLG + " (" + FRD_PRN_FLG.Length + ")\r\n"
                                + "CAVV_AAV_RSLT_CDE: " + CAVV_AAV_RSLT_CDE + " (" + CAVV_AAV_RSLT_CDE.Length + ")\r\n"
                                ;

        }
    }
}
