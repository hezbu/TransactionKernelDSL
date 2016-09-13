using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993.Tokens._Q6_PART_CHGS_TKN
{
    public sealed class _Q6_PART_CHGS_TKN : Iso8583_1993AbstractToken
    {
        private string _DLY = "  ";
        private string _INST = "  ";
        private PLAN_Values _PLAN = PLAN_Values.NoInterestChargeToCardholder;

        /// <summary>
        /// Diferimiento (2 byte long) - Numero de meses en que el pago no se hará exigible (Compre hoy y pague después), justificado con ceros a la izquierda.
        /// </summary>
        public string DLY
        {
            get
            {
                if (String.IsNullOrEmpty(_DLY)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNQ6_DLY);
                return _DLY;
            }
            set
            {
                if (value.Length != 2) throw new ApplicationException(Properties.Resources.ErrorSet_TKNQ6_DLY);
                _DLY = value;
            }
        }
        /// <summary>
        /// Parcialización (2 byte long) - Número de meses en que se van a dividir los pagos (con o sin intereses), justificado con ceros a la izquierda.
        /// </summary>
        public string INST
        {
            get
            {
                if (String.IsNullOrEmpty(_INST)) throw new ApplicationException(Properties.Resources.ErrorGet_TKNQ6_INST);
                return _INST;
            }
            set
            {
                if (value.Length != 2) throw new ApplicationException(Properties.Resources.ErrorSet_TKNQ6_INST);
                _INST = value;
            }
        }

        /// <summary>
        /// Tipo de Plan
        /// Values:
        /// 03 Sin intereses al Tarjetahabiente
        /// 05 Con intereses para el Tarjetahabiente
        /// 07 Compre hoy y pague después
        /// </summary>
        public string PLAN
        {
            get { return _PLAN.ToString("D").PadLeft(2, '0'); }
            set
            {
                if (value.Length != 2) throw new ApplicationException(Properties.Resources.ErrorSet_TKNQ6_PLAN);
                _PLAN = (PLAN_Values)Enum.Parse(typeof(PLAN_Values), value);
            }
        }

        public _Q6_PART_CHGS_TKN()
        {
            _TokenId = "Q6";
        }

        public _Q6_PART_CHGS_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ6 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ6 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ6 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ6 + "(3)");
            if (assembledToken.Substring(10).Length != 6) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ6 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ6 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ6 + "(6)");
        }

        public override bool AssembleToken()
        {
            _TokenData = DLY + INST + PLAN;
            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 6)
            {
                _ErrorMessage = "El payload para el token _Q6_PART_CHGS_TKN no es 6, es " + payload.Length;
            }
            else
            {
                try
                {
                    DLY = payload.Substring(0, 2);
                    INST = payload.Substring(0 + 2, 2);
                    PLAN = payload.Substring(0 + 2 + 2, 2);
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
            return base.Dump() + "DLY: " + DLY + " (" + DLY.Length + ")\r\n"
                               + "INST: " + INST + " (" + INST.Length + ")\r\n"
                               + "PLAN: " + PLAN + " (" + PLAN.Length + ") -> " + _PLAN + "\r\n";
        }

        public enum PLAN_Values
        {
            /// <summary>
            /// Sin intereses al Tarjetahabiente
            /// </summary>
            NoInterestChargeToCardholder = 3,
            /// <summary>
            /// Con intereses para el Tarjetahabiente
            /// </summary>
            InterestChargeToCardholder = 5,
            /// <summary>
            /// Compre hoy y pague después
            /// </summary>
            BuyTodayPayAfterwards = 7
        }
    }
}
