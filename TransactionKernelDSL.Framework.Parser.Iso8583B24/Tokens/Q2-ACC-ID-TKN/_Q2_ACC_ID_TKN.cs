using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583B24.Tokens._Q2_ACC_ID_TKN
{
    public sealed class _Q2_ACC_ID_TKN : Iso8583B24AbstractToken
    {
        private ACC_ID_Values _ACC_ID = ACC_ID_Values.TraditionalInnerNet;

        /// <summary>
        /// Access point identifier (2 bytes length mandatory)
        /// Values:
        /// 
        /// 01 Autorización Voz (Operador).
        /// 02 Cargos Automáticos a través de la Interred.
        /// 04 Interred Tradicional.
        /// 08 Ventas por Correo/Teléfono (MO/TO) a través de la Interred.
        /// 09 Internet (Comercio Electrónico) a través de la Interred.
        /// 14 Audio-Respuesta (IVR).
        /// 17 Comercios Multicaja.
        /// 19 CAT a través de la Interred.
        /// </summary>
        public string ACC_ID
        {
            get { return _ACC_ID.ToString("D").PadLeft(2, '0'); }
            set
            {
                if (value.Length != 2) throw new ApplicationException(Properties.Resources.ErrorSet_TKNQ2_ACC_ID);
                _ACC_ID = (ACC_ID_Values)Enum.Parse(typeof(ACC_ID_Values), value);
            }
        }

        public _Q2_ACC_ID_TKN()
        {
            _TokenId = "Q2";
        }

        public _Q2_ACC_ID_TKN(string assembledToken)
            : this()
        {
            if (assembledToken.StartsWith(_EyeCatcher) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ2 + "(0)");
            if (assembledToken.Length < 10) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ2 + "(1)");
            if (assembledToken.Substring(2, 2) != _TokenId) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ2 + "(2)");
            if (Convert.ToInt32(assembledToken.Substring(4, 5)) != assembledToken.Substring(10).Length) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ2 + "(3)");
            if (assembledToken.Substring(10).Length != 2) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ2 + "(4)");
            if (this.DisassembleToken(assembledToken.Substring(10)) == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ2 + "(5) " + _ErrorMessage);
            if (this.AssembleToken() == false) throw new ApplicationException(Properties.Resources.ErrorCtor_TKNQ2 + "(6)");
        }

        public override bool AssembleToken()
        {
            _TokenData = ACC_ID;
            return true;
        }

        public override bool DisassembleToken(string payload)
        {
            bool retVal = false;
            if (payload.Length != 2)
            {
                _ErrorMessage = "El payload para el token _Q2_ACC_ID_TKN no es 2, es " + payload.Length;
            }
            else
            {
                try
                {
                    ACC_ID = payload.Substring(0, 2);
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
            return base.Dump() + "ACC_ID: " + ACC_ID + " (" + ACC_ID.Length + ") -> " + _ACC_ID + "\r\n";
        }

        public enum ACC_ID_Values
        {
            /// <summary>
            /// Autorización Voz (Operador).
            /// </summary>
            OperVoiceAuth = 1,
            /// <summary>
            /// Cargos Automáticos a través de la Interred.
            /// </summary>
            AutomaticChargesInnerNet = 2,
            /// <summary>
            /// Interred Tradicional.
            /// </summary>
            TraditionalInnerNet = 4,
            /// <summary>
            /// Ventas por Correo/Teléfono (MO/TO) a través de la Interred.
            /// </summary>
            MOTOInnerNet = 8,
            /// <summary>
            /// Internet (Comercio Electrónico) a través de la Interred.
            /// </summary>
            InternetInnerNet = 9,
            /// <summary>
            /// Audio-Respuesta (IVR).
            /// </summary>
            IVR = 14,
            /// <summary>
            /// Comercios Multicaja.
            /// </summary>
            MultiCashRegisters = 17,
            /// <summary>
            /// CAT a través de la Interred.
            /// </summary>
            CATInnerNet = 19
        }
    }
}
