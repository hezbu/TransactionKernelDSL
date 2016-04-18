using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTransactionParserField
    {
        #region Member Properties
        public int Id { get; set; }
        public string Keyname { get; set; }
        public string Alias { get; set; }
        public AbstractTransactionParserFieldType Type { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public bool IsTransactionIdentifier { get; set; }
        public bool IsRequired { get; set; }
        public byte[] Content { get; set; }
        public Dictionary<int, AbstractTransactionParserSubfield> Subfields { get; set; }
        public bool HasValue
        {
            get
            {
                if (this.Content.Length > 0) return this.Content[0] != 0x00;
                else return false;
            }
        }
        #endregion


        public AbstractTransactionParserField()
        {
            Alias = null;
            Subfields = new Dictionary<int, AbstractTransactionParserSubfield>();
        }

        #region Virtual Methods And Properties
        public virtual AbstractTransactionParserSubfield this[int Id]
        {
            get
            {
                return Subfields[Id];
            }
        }
        public virtual AbstractTransactionParserSubfield this[string keyname]
        {
            get
            {
                foreach (AbstractTransactionParserSubfield subfield in Subfields.Values)
                {
                    if (subfield.Keyname == keyname) return subfield;
                }

                return null;
            }
        }
        public virtual void CreateContent()
        {
            switch (Type)
            {
                case AbstractTransactionParserFieldType.BCD: //"BCD"
                    this.Content = new byte[(this.Length * 2) + 1];
                    break;
                default:
                    this.Content = new byte[this.Length + 1];
                    break;
            }
        }

        public virtual int CopyContentFrom(byte[] src, int offset = 0)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");

            switch (Type)
            {
                case AbstractTransactionParserFieldType.BCD: //"BCD":
                    AbstractTransactionFacade.BcdToAsc(this.Content, src, this.Length * 2, 0, offset);
                    break;
                case AbstractTransactionParserFieldType.BIN: //"BIN":
                case AbstractTransactionParserFieldType.ASCII: //"ASCII":
                    Buffer.BlockCopy(src, offset, this.Content, 0, this.Length);
                    break;
                case AbstractTransactionParserFieldType.LVAR:
                    {
                        byte[] rawLVar = new byte[1];
                        byte[] rawLVarAscii = new byte[2];
                        byte[] aux = new byte[this.Content.Length];
                        byte[] auxBcd = new byte[this.Content.Length];
                        byte[] auxAscii = new byte[this.Content.Length * 2];
                        byte[] auxAsciiBeforeSeparator = new byte[this.Content.Length * 2];
                        byte[] auxAsciiAfterSeparator = new byte[this.Content.Length * 2];

                        System.Buffer.BlockCopy(src, offset, rawLVar, 0, 1);
                        AbstractTransactionFacade.BcdToAsc(rawLVarAscii, rawLVar, 2, 0);
                        string strLVar = AbstractTransactionFacade.GetString(rawLVarAscii, 2);

                        int intFieldLen = 0;
                        if (Int32.TryParse(strLVar, out intFieldLen) == false) throw new ApplicationException("LVAR length at _IsServerEngine " + this.Keyname + " is not numeric ->" + strLVar);
                        if (intFieldLen < this.Content.Length)
                        {
                            this.Length = 1 + intFieldLen;
                            this.Content = new byte[this.Length + 1];

                            System.Buffer.BlockCopy(src, offset + 1, aux, 0, (intFieldLen / 2)
                                                                       + (intFieldLen % 2)
                                                                       );

                            if ((intFieldLen % 2) > 0)
                            {
                                aux[intFieldLen / 2] &= 0xF0;
                            }

                            #region Find Separator and Build ASCII Track 2
                            bool separatorFound = false;

                            for (int i = 0; i < (intFieldLen / 2) + (intFieldLen % 2); i++)
                            {
                                if ((aux[i] & 0x0F) == 0x0D)
                                {
                                    AbstractTransactionFacade.BcdToAsc(auxAsciiBeforeSeparator, aux, (i * 2) + 1, 0);
                                    auxAsciiBeforeSeparator[(i * 2) + 1] = (byte)'=';
                                    System.Buffer.BlockCopy(aux, i + 1, auxBcd, 0, (intFieldLen / 2) + (intFieldLen % 2) - (i + 1));
                                    AbstractTransactionFacade.BcdToAsc(auxAsciiAfterSeparator, auxBcd, intFieldLen - ((i + 1) * 2), 0);
                                    System.Buffer.BlockCopy(auxAsciiBeforeSeparator, 0, auxAscii, 0, ((i + 1) * 2));
                                    System.Buffer.BlockCopy(auxAsciiAfterSeparator, 0, auxAscii, ((i + 1) * 2), intFieldLen - ((i + 1) * 2));
                                    separatorFound = true;
                                    break;
                                }
                                else if ((aux[i] & 0xF0) == 0xD0)
                                {
                                    AbstractTransactionFacade.BcdToAsc(auxAsciiBeforeSeparator, aux, i * 2, 0);
                                    auxAsciiBeforeSeparator[i * 2] = (byte)'=';
                                    System.Buffer.BlockCopy(aux, i, auxBcd, 0, (intFieldLen / 2) + (intFieldLen % 2) - i);
                                    auxBcd[0] = (byte)(auxBcd[0] & 0x0F);
                                    AbstractTransactionFacade.BcdToAsc(auxAsciiAfterSeparator, auxBcd, intFieldLen - (i * 2), 0);
                                    System.Buffer.BlockCopy(auxAsciiBeforeSeparator, 0, auxAscii, 0, (i * 2) + 1);
                                    System.Buffer.BlockCopy(auxAsciiAfterSeparator, 1, auxAscii, (i * 2) + 1, intFieldLen - (i * 2));
                                    separatorFound = true;
                                    break;
                                }
                            }
                            if (separatorFound == false)
                            {
                                AbstractTransactionFacade.BcdToAsc(auxAscii, aux, intFieldLen, 0);
                            }
                            #endregion

                            System.Buffer.BlockCopy(rawLVar, 0, this.Content, 0, 1);
                            System.Buffer.BlockCopy(auxAscii, 0, this.Content, 1, intFieldLen);

                            return 1 + (intFieldLen / 2) + (intFieldLen % 2);
                        }
                        else throw new Exception("Invalid LLVAR Length for " + this.Keyname + ": " + intFieldLen.ToString());
                    }
                case AbstractTransactionParserFieldType.LLVAR:
                    {
                        byte[] rawLLVar = new byte[2];
                        byte[] rawLLVarAscii = new byte[4];
                        System.Buffer.BlockCopy(src, offset, rawLLVar, 0, 2);
                        AbstractTransactionFacade.BcdToAsc(rawLLVarAscii, rawLLVar, 4, 0);
                        string strLLVar = AbstractTransactionFacade.GetString(rawLLVarAscii, 4);
                        int intFieldLen = 0;
                        if (Int32.TryParse(strLLVar, out intFieldLen) == false) throw new ApplicationException("LLVAR length at _IsServerEngine " + this.Keyname + " is not numeric ->" + strLLVar);
                        if (intFieldLen < this.Content.Length)
                        {
                            this.Length = intFieldLen + 2;
                            this.Content = new byte[this.Length + 1];
                            System.Buffer.BlockCopy(src, offset, this.Content, 0, intFieldLen + 2);
                            return this.Length;
                        }
                        else throw new Exception("Invalid LLVAR Length for " + this.Keyname + ": " + intFieldLen.ToString());
                    }

                case AbstractTransactionParserFieldType.ASCII_LVAR:
                    {
                     byte[] rawLLVar = new byte[1];
                     byte[] rawLLVarAscii = new byte[2];
                     System.Buffer.BlockCopy(src, offset, rawLLVar, 0, 1);
                     AbstractTransactionFacade.BcdToAsc(rawLLVarAscii, rawLLVar, 2, 0);
                     string strLLVar = AbstractTransactionFacade.GetString(rawLLVarAscii, 2);
                     int intFieldLen = 0;
                     if (Int32.TryParse(strLLVar, out intFieldLen) == false) throw new ApplicationException("LVAR length at _IsServerEngine " + this.Keyname + " is not numeric ->" + strLLVar);
                     if (intFieldLen < this.Content.Length)
                     {
                      this.Length = intFieldLen + 1;
                      this.Content = new byte[this.Length + 1];
                      System.Buffer.BlockCopy(src, offset, this.Content, 0, intFieldLen + 1);
                      return this.Length;
                     }
                     else throw new Exception("Invalid LVAR Length for " + this.Keyname + ": " + intFieldLen.ToString());
                    }

                default: throw new ApplicationException("CopyContentFrom for type " + Type + " is not known");

            }

            return this.Length;
        }
        public virtual int CopyContentTo(ref byte[] dest, int offset = 0)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");
            switch (Type)
            {
                case AbstractTransactionParserFieldType.ASCII:
                case AbstractTransactionParserFieldType.BIN:
                case AbstractTransactionParserFieldType.LLVAR:
                case AbstractTransactionParserFieldType.ASCII_LVAR:
                    Buffer.BlockCopy(this.Content, 0, dest, offset, this.Length);
                    break;
                case AbstractTransactionParserFieldType.LVAR:
                    {
                        byte[] fieldPayloadInAscii = new byte[this.Length + 1];
                        byte[] fieldPayloadInBcd = new byte[this.Length + 1];
                        byte[] returnArray = new byte[this.Length + 1 + 1];


                        #region Get LVAR Length int value
                        byte[] rawLVar = new byte[1];
                        byte[] rawLVarAscii = new byte[2];
                        Buffer.BlockCopy(this.Content, 0, rawLVar, 0, 1);
                        AbstractTransactionFacade.BcdToAsc(rawLVarAscii, rawLVar, 2, 0);
                        int intFieldLen = 0;
                        if (Int32.TryParse(AbstractTransactionFacade.GetString(rawLVarAscii, 2), out intFieldLen) == false) throw new ApplicationException("LVAR length at _IsServerEngine " + this.Keyname + " is not numeric ->" + AbstractTransactionFacade.GetString(rawLVarAscii, 2));
                        #endregion

                        System.Buffer.BlockCopy(this.Content, 1, fieldPayloadInAscii, 0, intFieldLen);

                        bool track2SeparatorFound = false;
                        int track2SeparatorIndex = 0;
                        for (track2SeparatorIndex = 0; track2SeparatorIndex < this.Content.Length; track2SeparatorIndex++)
                        {
                            if (this.Content[track2SeparatorIndex] == 0x3d)
                            {
                                track2SeparatorFound = true;
                                break;
                            }
                        }

                        if (intFieldLen % 2 == 0)
                        {
                            AbstractTransactionFacade.AscToBcd(fieldPayloadInBcd, fieldPayloadInAscii, intFieldLen, 0);
                        }
                        else
                        {
                            fieldPayloadInAscii[intFieldLen] = 0x30;
                            AbstractTransactionFacade.AscToBcd(fieldPayloadInBcd, fieldPayloadInAscii, intFieldLen, 0);
                            fieldPayloadInBcd[intFieldLen / 2] |= 0x0F;
                        }

                        if (track2SeparatorFound == true)
                        {
                            if (track2SeparatorIndex % 2 == 0)
                            {
                                fieldPayloadInBcd[(track2SeparatorIndex / 2)-1] &= 0xF0;
                                fieldPayloadInBcd[(track2SeparatorIndex / 2)-1] |= 0x0D;
                            }
                            else
                            {
                                fieldPayloadInBcd[track2SeparatorIndex / 2] &= 0x0F;
                                fieldPayloadInBcd[track2SeparatorIndex / 2] |= 0xD0;
                            }
                        }

                        intFieldLen = (intFieldLen / 2) + (intFieldLen % 2);
                        fieldPayloadInBcd.CopyTo(returnArray, 1);
                        returnArray[0] = rawLVar[0];

                        if (intFieldLen < 999)
                        {
                            System.Buffer.BlockCopy(returnArray, 0, dest, offset, 1 + intFieldLen);
                            return 1 + intFieldLen;
                        }
                        else throw new Exception("Invalid LLVAR Length for " + this.Keyname + ": " + intFieldLen.ToString());
                    }

                case AbstractTransactionParserFieldType.BCD:
                    byte[] aux = new byte[(this.Length) + 1];
                    AbstractTransactionFacade.AscToHex(aux, this.Content, this.Length);
                    Buffer.BlockCopy(aux, 0, dest, offset, this.Length);
                    break;
                default: throw new ApplicationException("Type " + Type + " for CopyContentTo is not known");
            }

            return this.Length;
        }

        public virtual int TrueLength
        {
            get
            {
                switch (Type)
                {
                    case AbstractTransactionParserFieldType.ASCII:
                    case AbstractTransactionParserFieldType.BCD:
                    case AbstractTransactionParserFieldType.BIN:
                        return Length;
                    case AbstractTransactionParserFieldType.LVAR:
                        return Length - 1;
                    case AbstractTransactionParserFieldType.LLVAR:
                        return Length - 2;
                    default:
                        throw new ApplicationException("Type " + Type + " for TrueLength is not known");
                }
            }
        }
        #endregion
        public override string ToString()
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");
            return AbstractTransactionFacade.GetString(this.Content, this.Content.Length - 1);

        }
    }
}
