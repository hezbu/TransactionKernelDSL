
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.Parser.Iso8583_1993;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993
{
    public class Iso8583_1993Decoder
    {      
        private Iso8583_1993Parser _Parser;

        public Iso8583_1993Decoder(byte[] additionalData, int additionalDataLen)
        {            
            _Parser = new Iso8583_1993Parser("ISO", true);
            (_Parser.RequestStream as Iso8583_1993Stream).Set(additionalData, additionalDataLen);
        }

        public override string ToString()
        {
            var result = String.Empty;

            if (_Parser.Disassemble() == false) return "Error desarmando ISO8583_1993";

            var str = _Parser.RequestStructure as Iso8583_1993Structure;


            result += String.Format("\t - Start Signature:      {0}{1}", (str["StartSignature"] as Iso8583_1993Field).ToString(), Environment.NewLine);
            result += String.Format("\t - Header:               {0}{1}", (str["Base24Header"] as Iso8583_1993Field).ToString(), Environment.NewLine);
            result += String.Format("\t - SID:                  {0}{1}", AbstractTransactionFacade.Dump((str["SID"] as Iso8583_1993Field).Content, (str["SID"] as Iso8583_1993Field).Length), Environment.NewLine);
            result += String.Format("\t - DID:                  {0}{1}", AbstractTransactionFacade.Dump((str["DID"] as Iso8583_1993Field).Content, (str["DID"] as Iso8583_1993Field).Length), Environment.NewLine);
            result += String.Format("\t - LEN:                  {0}{1}", AbstractTransactionFacade.Dump((str["LENGTH"] as Iso8583_1993Field).Content, (str["LENGTH"] as Iso8583_1993Field).Length), Environment.NewLine);
            result += String.Format("\t - MessageType:          {0}{1}", (str["MessageType"] as Iso8583_1993Field).ToString(), Environment.NewLine);
            result += String.Format("\t - Primary BITMAP:       {0}{1}", AbstractTransactionFacade.Dump((str["PrimaryBitmap"] as Iso8583_1993Field).Content, (str["PrimaryBitmap"] as Iso8583_1993Field).Length), Environment.NewLine);

            for (int i = 1; i < 71; i++)
            {
                var field = (str[i.ToString()] as Iso8583_1993Field);
                if (field == null) continue;
                if (str.IsFieldOnBitmap(i) == false) continue;

                switch (field.Type)
                {
                    case TransactionKernelDSL.Framework.V1.AbstractTransactionParserFieldType.ASCII:
                        result += String.Format("\t - F{2:D3}:               {0}{1}", field.ToString(), Environment.NewLine, i);
                        break;
                    case TransactionKernelDSL.Framework.V1.AbstractTransactionParserFieldType.BCD:
                    case TransactionKernelDSL.Framework.V1.AbstractTransactionParserFieldType.BIN:
                    case TransactionKernelDSL.Framework.V1.AbstractTransactionParserFieldType.LLLVAR:
                    case TransactionKernelDSL.Framework.V1.AbstractTransactionParserFieldType.LLVAR:
                        result += String.Format("\t - F{2:D3}:               {0}{1}", AbstractTransactionFacade.Dump(field.Content, field.Length), Environment.NewLine, i);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
