using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993.Tokens
{
    public abstract class Iso8583_1993AbstractToken
    {
        public static char EyeCatcher = '!';

        protected const string _EyeCatcher = "! ";
        protected string _TokenId = null;
        protected string _TokenLength = null;
        protected string _UserFld = " ";

        protected string _TokenData = null;

        protected string _ErrorMessage = null;



        public abstract bool AssembleToken();
        public abstract bool DisassembleToken(string payload);

        public virtual string GetErrorMessage()
        {
            return _ErrorMessage;
        }
        public virtual bool IsAnyErrorPresent()
        {
            return !String.IsNullOrEmpty(_ErrorMessage);
        }


        public override string ToString()
        {
            if (this.AssembleToken() == false) throw new ApplicationException(_TokenId + " -> " + Properties.Resources.ErrorToString_TKN);
            return _EyeCatcher + _TokenId + _TokenData.Length.ToString().PadLeft(5, '0') + _UserFld + _TokenData;
        }

        public virtual string Dump()
        {
            if (this.AssembleToken() == false) throw new ApplicationException(_TokenId + " -> " + Properties.Resources.ErrorToString_TKN);
            return "Eye Catcher: " + _EyeCatcher + "\r\n" +
                    "Token Id: " + _TokenId + "\r\n" +
                    "Token Length: " + _TokenData.Length.ToString().PadLeft(5, '0') + "\r\n" +
                    "UserFld: " + _UserFld + "\r\n\r\n";
        }

    }
}
