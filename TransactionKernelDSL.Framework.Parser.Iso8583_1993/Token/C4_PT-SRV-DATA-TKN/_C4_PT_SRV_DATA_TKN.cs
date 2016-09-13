using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993.Tokens._C4_PT_SRV_DATA_TKN
{
    public sealed class _C4_PT_SRV_DATA_TKN : Iso8583_1993AbstractToken
    {
        private TERM_ATTEND_IND_Values _TERM_ATTEND_IND = TERM_ATTEND_IND_Values.AttendedTerminal;
        private const string _TERM_OPER_IND = "0"; // N/A
        private TERM_LOC_IND_Values _TERM_LOC_IND = TERM_LOC_IND_Values.OnPremisesOfCardAcceptorFacility;
        private CRDHLDR_PRESENT_IND_Values _CRDHLDR_PRESENT_IND = CRDHLDR_PRESENT_IND_Values.CardholderPresent;
        private CRD_PRESENT_IND_Values _CRD_PRESENT_IND = CRD_PRESENT_IND_Values.CardPresent;
        private CRD_CAPTR_IND_Values _CRD_CAPTR_IND = CRD_CAPTR_IND_Values.NoCardCaptureCapability;
        private TXN_STAT_IND_Values _TXN_STAT_IND = TXN_STAT_IND_Values.NormalRequest;
        private const string _TXN_SEC_IND = "0";  //  N/A
        private const string _TXN_RTN_IND = "0";  //  N/A
        private CRDHLDR_ACTVT_TERM_IND_Values _CRDHLDR_ACTVT_TERM_IND = CRDHLDR_ACTVT_TERM_IND_Values.NotCATTransaction;
        private TERM_INPUT_CAP_IND_Values _TERM_INPUT_CAP_IND = TERM_INPUT_CAP_IND_Values.Unknown;
        private CRDHLDR_ID_METHOD_Values _CRDHLDR_ID_METHOD = CRDHLDR_ID_METHOD_Values.Unknown;

        #region Properties
        /// <summary>
        /// It indicates Terminal Type
        /// Values:
        /// 0 = attended terminal
        /// 1 = cardholder-activated terminal   [CAT], home PC)
        /// 2 = no terminal used (voice/ARU authorization)
        /// </summary>
        public string TERM_ATTEND_IND
        {
            get
            {
                return _TERM_ATTEND_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_TERM_ATTEND_IND);
                _TERM_ATTEND_IND = (TERM_ATTEND_IND_Values)Enum.Parse(typeof(TERM_ATTEND_IND_Values), value);
            }
        }
        /// <summary>
        /// N/A
        /// </summary>
        public string TERM_OPER_IND
        {
            get
            {
                return _TERM_OPER_IND;
            }
        }
        /// <summary>
        /// Terminal Location.
        /// Values:
        /// 0 = On premises of card acceptor facility
        /// 1 = Off premises of card acceptor facility
        ///       (Merchant terminal--remote location)
        /// 2 = On premises of cardholder (home PC)
        /// 3 = No terminal used (voice/ARU authorization)
        /// </summary>
        public string TERM_LOC_IND
        {
            get
            {
                return _TERM_LOC_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_TERM_LOC_IND);
                _TERM_LOC_IND = (TERM_LOC_IND_Values)Enum.Parse(typeof(TERM_LOC_IND_Values), value);
            }
        }
        /// <summary>
        /// Cardholder Indicator.
        /// Values:
        /// 0 = cardholder present
        /// 1 = cardholder not present, unspecified
        /// 2 = cardholder not present, mail/facsimile order
        /// 3 = cardholder not present, telephone/ARU order
        /// 4 = cardholder not present, recurring transaction
        /// 5 = Electronic order (home PC, Internet)
        /// </summary>
        public string CRDHLDR_PRESENT_IND
        {
            get
            {
                return _CRDHLDR_PRESENT_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_CRDHLDR_PRESENT_IND);
                _CRDHLDR_PRESENT_IND = (CRDHLDR_PRESENT_IND_Values)Enum.Parse(typeof(CRDHLDR_PRESENT_IND_Values), value);
            }
        }
        /// <summary>
        /// Card Indicator
        /// Values 
        /// 0 	= 	card present
        /// 1 	=	 card not present
        /// </summary>
        public string CRD_PRESENT_IND
        {
            get
            {
                return _CRD_PRESENT_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_CRD_PRESENT_IND);
                _CRD_PRESENT_IND = (CRD_PRESENT_IND_Values)Enum.Parse(typeof(CRD_PRESENT_IND_Values), value);
            }
        }
        /// <summary>
        /// It indicates whether terminal can capture the card.
        /// Values 
        /// 0 	= 	card present
        /// 1 	=	 card not present
        /// </summary>
        public string CRD_CAPTR_IND
        {
            get
            {
                return _CRD_CAPTR_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_CRD_CAPTR_IND);
                _CRD_CAPTR_IND = (CRD_CAPTR_IND_Values)Enum.Parse(typeof(CRD_CAPTR_IND_Values), value);
            }
        }
        /// <summary>
        /// Transaction Status.
        /// Values 
        /// 0 	= 	card present
        /// 1 	=	 card not present
        /// </summary>
        public string TXN_STAT_IND
        {
            get
            {
                return _TXN_STAT_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_TXN_STAT_IND);
                _TXN_STAT_IND = (TXN_STAT_IND_Values)Enum.Parse(typeof(TXN_STAT_IND_Values), value);
            }
        }
        /// <summary>
        /// N/A
        /// </summary>
        public string TXN_SEC_IND
        {
            get
            {
                return _TXN_SEC_IND;
            }
        }
        /// <summary>
        /// N/A
        /// </summary>
        public string TXN_RTN_IND
        {
            get
            {
                return _TXN_RTN_IND;
            }
        }
        /// <summary>
        /// CAT Indicator.
        /// Values:
        /// 0 = not a CAT transaction
        /// 1 = automated dispensing machine with PIN/level 1
        /// 2 = self-service terminal/level 2
        /// 3 = limited amount terminal/level 3
        /// 4 = in-flight commerce/level 4
        /// 5 = script device
        /// 6 = electronic commerce
        /// 7 = radio frequency device
        /// </summary>
        public string CRDHLDR_ACTVT_TERM_IND
        {
            get
            {
                return _CRDHLDR_ACTVT_TERM_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_CRDHLDR_ACTVT_TERM_IND);
                _CRDHLDR_ACTVT_TERM_IND = (CRDHLDR_ACTVT_TERM_IND_Values)Enum.Parse(typeof(CRDHLDR_ACTVT_TERM_IND_Values), value);
            }
        }
        /// <summary>
        /// Means of entering credit card number as used at Terminal.
        /// Values:
        /// 0 = unknown or unspecified
        /// 1 = no terminal used (voice/ARU authorization)
        /// 2 = magnetic stripe reader
        /// 3 = bar code (reserved for future use)
        /// 4 = optical character recognition (rsrvd for future use)
        /// 5 = magnetic stripe reader and EMV compatible 
        /// 6 = key entry only
        /// 7 = magnetic stripe reader and key entry
        /// 8 = magnetic stripe reader, key entry, and EMV 
        /// 9 = EMV compatible ICC reader
        /// </summary>
        public string TERM_INPUT_CAP_IND
        {
            get
            {
                return _TERM_INPUT_CAP_IND.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_TERM_INPUT_CAP_IND);
                _TERM_INPUT_CAP_IND = (TERM_INPUT_CAP_IND_Values)Enum.Parse(typeof(TERM_INPUT_CAP_IND_Values), value);
            }
        }
        /// <summary>
        /// Cardholder Authentication MultiThreadedListenerTransactionEngineMaster.
        /// Values:
        /// 0 	= 	Unknown (default)
        /// 1	=	 Signature
        /// 2	=	 PIN
        /// 3	=	 Unattended Terminal
        /// 4	=	 Mail/Phone Order
        /// </summary>
        public string CRDHLDR_ID_METHOD
        {
            get
            {
                return _CRDHLDR_ID_METHOD.ToString("D");
            }
            set
            {
                if (value.Length != 1) throw new ApplicationException(Properties.Resources.ErrorSet_TKNC4_CRDHLDR_ID_METHOD);
                _CRDHLDR_ID_METHOD = (CRDHLDR_ID_METHOD_Values)Enum.Parse(typeof(CRDHLDR_ID_METHOD_Values), value);
            }
        }
        #endregion

        #region Constructor
        public _C4_PT_SRV_DATA_TKN()
        {
            _TokenId = "C4";

        }

        public _C4_PT_SRV_DATA_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC4 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC4 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC4 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC4 + "(3)");
            if (assembledToken.Substring(10).Length != 12) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC4 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC4 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNC4 + "(6)");
        }
        #endregion

        public override bool AssembleToken()
        {
            _TokenData = TERM_ATTEND_IND + TERM_OPER_IND + TERM_LOC_IND + CRDHLDR_PRESENT_IND + CRD_PRESENT_IND +
                         CRD_CAPTR_IND + TXN_STAT_IND + TXN_SEC_IND + TXN_RTN_IND + CRDHLDR_ACTVT_TERM_IND + TERM_INPUT_CAP_IND +
                         CRDHLDR_ID_METHOD;
            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 12)
            {
                _ErrorMessage = "El payload para el token C4_PT_SRV_DATA_TKN no es 12, es " + payload.Length;
            }
            else
            {
                try
                {
                    TERM_ATTEND_IND = payload.Substring(0, 1);
                    //TERM_OPER_IND = payload.Substring(0 + 1, 1);
                    TERM_LOC_IND = payload.Substring(0 + 1 + 1, 1);
                    CRDHLDR_PRESENT_IND = payload.Substring(0 + 1 + 1 + 1, 1);
                    CRD_PRESENT_IND = payload.Substring(0 + 1 + 1 + 1 + 1, 1);
                    CRD_CAPTR_IND = payload.Substring(0 + 1 + 1 + 1 + 1 + 1, 1);
                    TXN_STAT_IND = payload.Substring(0 + 1 + 1 + 1 + 1 + 1 + 1, 1);
                    //TXN_SEC_IND = payload.Substring(0 + 1 + 1 + 1 + 1 + 1 + 1 + 1, 1);
                    //TXN_RTN_IND = payload.Substring(0 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1, 1);
                    CRDHLDR_ACTVT_TERM_IND = payload.Substring(0 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1, 1);
                    TERM_INPUT_CAP_IND = payload.Substring(0 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1, 1);
                    CRDHLDR_ID_METHOD = payload.Substring(0 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1, 1);

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
            return base.Dump() + "TERM_ATTEND_IND: " + TERM_ATTEND_IND + " (" + TERM_ATTEND_IND.Length + ") -> " + _TERM_ATTEND_IND + "\r\n"
                                + "TERM_OPER_IND: " + TERM_OPER_IND + " (" + TERM_OPER_IND.Length + ")\r\n"
                                + "TERM_LOC_IND: " + TERM_LOC_IND + " (" + TERM_LOC_IND.Length + ") -> " + _TERM_LOC_IND + "\r\n"
                                + "CRDHLDR_PRESENT_IND: " + CRDHLDR_PRESENT_IND + " (" + CRDHLDR_PRESENT_IND.Length + ") -> " + _CRDHLDR_PRESENT_IND + "\r\n"
                                + "CRD_PRESENT_IND: " + CRD_PRESENT_IND + " (" + CRD_PRESENT_IND.Length + ") -> " + _CRD_PRESENT_IND + "\r\n"
                                + "CRD_CAPTR_IND: " + CRD_CAPTR_IND + " (" + CRD_CAPTR_IND.Length + ") -> " + _CRD_CAPTR_IND + "\r\n"
                                + "TXN_STAT_IND: " + TXN_STAT_IND + " (" + TXN_STAT_IND.Length + ") -> " + _TXN_STAT_IND + "\r\n"
                                + "TXN_SEC_IND: " + TXN_SEC_IND + " (" + TXN_SEC_IND.Length + ")\r\n"
                                + "TXN_RTN_IND: " + TXN_RTN_IND + " (" + TXN_RTN_IND.Length + ")\r\n"
                                + "CRDHLDR_ACTVT_TERM_IND: " + CRDHLDR_ACTVT_TERM_IND + " (" + CRDHLDR_ACTVT_TERM_IND.Length + ") -> " + _CRDHLDR_ACTVT_TERM_IND + "\r\n"
                                + "TERM_INPUT_CAP_IND: " + TERM_INPUT_CAP_IND + " (" + TERM_INPUT_CAP_IND.Length + ") -> " + _TERM_INPUT_CAP_IND + "\r\n"
                                + "CRDHLDR_ID_METHOD: " + CRDHLDR_ID_METHOD + " (" + CRDHLDR_ID_METHOD.Length + ") -> " + _CRDHLDR_ID_METHOD + "\r\n"
                                ;

        }


        public enum TERM_ATTEND_IND_Values
        {
            AttendedTerminal = 0,
            CardHolder_ActivatedTerminal = 1,
            NoTerminalUsed = 2
        }

        public enum TERM_LOC_IND_Values
        {
            OnPremisesOfCardAcceptorFacility = 0,
            OffPremisesOfCardAcceptorFacility = 1,
            OnPremisesOfCardholder = 2,
            NoTerminalUsed = 3
        }

        public enum CRDHLDR_PRESENT_IND_Values
        {
            CardholderPresent = 0,
            CardholderNotPresent_Unspecified = 1,
            CardholderNotPresent_MailOrFacsimileOrder = 2,
            CardholderNotPresent_TelephoneOrARUOrder = 3,
            CardholderNotPresent_RecurringTransaction = 4,
            ElectronicOrder = 5
        }

        public enum CRD_PRESENT_IND_Values
        {
            CardPresent = 0,
            CardNotPresent = 1
        }

        public enum CRD_CAPTR_IND_Values
        {
            NoCardCaptureCapability = 0,
            CardCaptureCapability = 1
        }

        public enum TXN_STAT_IND_Values
        {
            NormalRequest = 0,
            MerchantAuthorization = 1,
            PreAuthorizedRequest = 4,
            StandIn = 5,
            AddressVerificationRequest = 6,
            CashBack = 7,
            DowntimeSubmissionRequest = 8
        }

        public enum CRDHLDR_ACTVT_TERM_IND_Values
        {
            NotCATTransaction = 0,
            AutomatedDispensingMachineWithPINLevel1 = 1,
            SelfServiceTerminalLevel2 = 2,
            LimitedAmountTerminalLevel3 = 3,
            InFlightCommerceLevel4 = 4,
            ScriptDevice = 5,
            ElectronicCommerce = 6,
            RadioFrequencyDevice = 7
        }

        public enum TERM_INPUT_CAP_IND_Values
        {
            Unknown = 0,
            NoTerminalUsed = 1,
            MagneticStripeReader = 2,
            BarCode = 3,
            OpticalCharacterRecognition = 4,
            MagneticStripeReaderAndEMVCompatible = 5,
            KeyEntryOnly = 6,
            MagneticStripeReaderAndKeyEntry = 7,
            MagneticStripeReaderAndKeyEntryAndEMV = 8,
            EMVCompatibleICCReader = 9
        }

        public enum CRDHLDR_ID_METHOD_Values
        {
            Unknown = 0,
            Signature = 1,
            PIN = 2,
            UnattendedTerminal = 3,
            Mail_PhoneOrder = 4,
            QPS_Transaction = 5
        }
    }
}
