using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Meflur
{
    public class MeflurStream : AbstractTransactionParserStream
    {
        public const byte STX = 0x02;
        public const byte EOF = 0x04;
        public const byte ETX = 0x03;

        public const int MeflurMaxLength = 8192;
        private int _Length;

        public int Length
        {
            get { return _Length; }           
        }

        public MeflurStream() :
            base(MeflurMaxLength)
        {

        }

        public override void Set(byte[] stream, int? length = null)
        {
            if (length.HasValue == false) _Length = stream.Length;
            else _Length = length.Value;
             
            base.Set(stream, length);
        }

        public override string ToString()
        {
            string strResult = "";

            for (int i = 0; i < this._Stream.Length; i++)
            {
                if (this._Stream[i] == 0x02 || this._Stream[i] == 0x03 || this._Stream[i] == 0x04)
                {
                    strResult += "(" + this._Stream[i].ToString("X").PadLeft(2, '0') + ")";
                }
                else
                {
                    strResult += ((char)this._Stream[i]).ToString();
                }
            }

            return strResult;
        }

        #region Owned Methods
        public string[] ParseFields()
        {
            return this.ToString().Split(new char[] { (char)MeflurStream.EOF });
        }
        public bool Validate()
        {
            bool etxFound = false;

            if (this._Stream[0] != MeflurStream.STX) 
            {
                return false;
            }

            for (int i = 0; i < this._Stream.Length; i++)
            {
                if (this._Stream[i] == MeflurStream.ETX)
                {
                    etxFound = true;
                    break;
                }

            }

            return etxFound;
        }
        #endregion
    }
}
