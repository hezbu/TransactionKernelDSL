using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993
{
    public sealed class Iso8583_1993Structure : AbstractTransactionParserStructure
    {
        #region Member Field
        private bool _IsDataPrefix = false;
        private bool _IsImsCicsTransactionCode = false;
        private string _PrimaryBitmapKeyname = null;
        private string _SecondaryBitmapKeyname = null;
        #endregion

        #region Member Properties
        public bool IsDataPrefix
        {
            get { return _IsDataPrefix; }
        }
        public bool IsImsCicsTransactionCode
        {
            get { return _IsImsCicsTransactionCode; }
        }
        #endregion

        #region Constructor
        public Iso8583_1993Structure(Nullable<AbstractTransactionParserStructureType> type,
                               string rootSection = "Iso8583_1993",
                               string primaryBitmapKeyname = "PrimaryBitmap",
                               string secondaryBitmapKeyname = "1",
                               bool isDataPrefix = false,
                               bool isImsCicsTransactionCode = false)
            : base(rootSection, type)
        {
            _PrimaryBitmapKeyname = primaryBitmapKeyname;
            _SecondaryBitmapKeyname = secondaryBitmapKeyname;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AbstractTransactionParserSection defaultLayout = config.GetSection(rootSection) as AbstractTransactionParserSection;

            if (defaultLayout == null) throw new ApplicationException("ConfigSection " + rootSection + " not found on app.config!");

            #region Select Default Fields by type
            FieldsCollection defaultFields = null;
            switch (type)
            {
                #region Default Request Fields
                case AbstractTransactionParserStructureType.Request:
                    defaultFields = defaultLayout.DefaultRequestFields;
                    break;
                #endregion
                #region Default Response Fields
                case AbstractTransactionParserStructureType.Response:
                    defaultFields = defaultLayout.DefaultResponseFields;
                    break;
                #endregion
                default: throw new ApplicationException("Structure Type " + type + "not found on " + rootSection + " structure");
            }
            #endregion
            #region Adding Default Fields
            foreach (FieldElement f in defaultFields)
            {
                Iso8583_1993Field newField = new Iso8583_1993Field()
                {
                    Id = f.Id,
                    Keyname = f.Keyname,
                    Type = (AbstractTransactionParserFieldType)Enum.Parse(typeof(AbstractTransactionParserFieldType), f.Type),
                    Length = f.Length,
                    IsRequired = f.IsRequired,
                    IsTransactionIdentifier = f.IsTransactionIdentifier,
                    Description = f.Description
                };

                newField.CreateContent();
                if (!String.IsNullOrEmpty(f.DefaultValue)) { newField.CopyContentFrom(AbstractTransactionFacade.GetBytes(f.DefaultValue)); }
                Fields.Add(newField.Id, newField);
            }
            #endregion

            _IsDataPrefix = isDataPrefix;
            _IsImsCicsTransactionCode = isImsCicsTransactionCode;
        }
        #endregion


        #region Owned Methods
        public int FindISOStart(byte[] requestStream)
        {
            int intReturn = 0;
            for (int i = 0; i < requestStream.Length; i++)
            {
                if (
                    requestStream[i] == (byte)'I' &&
                    requestStream[i + 1] == (byte)'S' &&
                    requestStream[i + 2] == (byte)'O'
                    )
                {
                    intReturn = i;
                    break;
                }
            }

            if (intReturn == requestStream.Length)
            {
                throw new ApplicationException("Signature 'ISO' haven't found on stream");
            }

            return intReturn;
        }
        public byte GetBitmapByByte(int intBmpIndex)
        {
            if (intBmpIndex > 16 || intBmpIndex < 0)
                throw new ArgumentException("Bitmap index must be within 0 and 8, but never " + intBmpIndex.ToString());


            byte byteToReturn = new byte();

            if (intBmpIndex >= 8)  //Usa el segundo Bitmap
            {
                byteToReturn = this[_SecondaryBitmapKeyname].Content[intBmpIndex - 8];//AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_SecondaryBitmapKeyname].Content, 2, (intBmpIndex - 8) * 2));
                return byteToReturn;
            }
            else //Usa el primer Bitmap
            {
                byteToReturn = this[_PrimaryBitmapKeyname].Content[intBmpIndex];//AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_SecondaryBitmapKeyname].Content, 2, (intBmpIndex - 8) * 2));
                return byteToReturn;
                //byteToReturn = AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_PrimaryBitmapKeyname].Content, 2, intBmpIndex * 2));
                //return byteToReturn[0];
            }

        }
        public bool IsIso8583RequestMsgType()
        {
            bool isRequest = false; //Presumimos que es de 64 bits.         
            byte byteAnalisys = (byte)((int)this["MessageType"].Content[2] - 0x30);

            if ((((int)byteAnalisys) & 0x01) == 0x00)
            {
                isRequest = true;
            }

            return isRequest;
        }
        public void TurnOnField(int fieldParam)
        {
            byte mask = 0x80;

            if (fieldParam < 1 || fieldParam > 128)
            {
                throw new ArgumentOutOfRangeException("Field number is out of range (" + fieldParam.ToString() + "vs 1 - 128)");
            }

            byte[] byteToReturn = new byte[1 + 1];
            int indexBitmapByte = (fieldParam - 1) / 8;
            int indexBitmapBit = (fieldParam - 1) % 8;

            if (fieldParam >= 64)  //Usa el segundo Bitmap
            {
                byteToReturn = AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_SecondaryBitmapKeyname].Content, 2, (indexBitmapByte - 8) * 2));
            }
            else //Usa el primer Bitmap
            {
                byteToReturn = AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_PrimaryBitmapKeyname].Content, 2, indexBitmapByte * 2));
            }

            byteToReturn[0] |= (byte)(((int)mask) >> indexBitmapBit);

            if (fieldParam >= 64)  //Usa el segundo Bitmap
            {
                AbstractTransactionFacade.BcdToAsc(this[_SecondaryBitmapKeyname].Content, byteToReturn, 2, 0, 0, (indexBitmapByte - 8) * 2);
            }
            else //Usa el primer Bitmap
            {
                AbstractTransactionFacade.BcdToAsc(this[_PrimaryBitmapKeyname].Content, byteToReturn, 2, 0, 0, indexBitmapByte * 2);
            }


        }
        public void TurnOffField(int fieldParam)
        {
            byte mask = 0x80;

            if (fieldParam < 1 || fieldParam > 128)
            {
                throw new ArgumentOutOfRangeException("Field number is out of range (" + fieldParam.ToString() + "vs 1 - 128)");
            }

            byte[] byteToReturn = new byte[1 + 1];
            int indexBitmapByte = (fieldParam - 1) / 8;
            int indexBitmapBit = (fieldParam - 1) % 8;

            if (fieldParam >= 64)  //Usa el segundo Bitmap
            {
                byteToReturn = AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_SecondaryBitmapKeyname].Content, 2, (indexBitmapByte - 8) * 2));
            }
            else //Usa el primer Bitmap
            {
                byteToReturn = AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_PrimaryBitmapKeyname].Content, 2, indexBitmapByte * 2));
            }

            byteToReturn[0] &= (byte)(~(((int)mask) >> indexBitmapBit));

            if (fieldParam >= 64)  //Usa el segundo Bitmap
            {
                AbstractTransactionFacade.BcdToAsc(this[_SecondaryBitmapKeyname].Content, byteToReturn, 2, 0, 0, (indexBitmapByte - 8) * 2);
            }
            else //Usa el primer Bitmap
            {
                AbstractTransactionFacade.BcdToAsc(this[_PrimaryBitmapKeyname].Content, byteToReturn, 2, 0, 0, indexBitmapByte * 2);
            }
        }
        public bool IsFieldOnBitmap(int fieldParam)
        {
            byte mask = 0x80;

            if (fieldParam < 1 || fieldParam > 128)
            {
                throw new ArgumentOutOfRangeException("Field number is out of range (" + fieldParam.ToString() + "vs 1 - 128)");
            }

            byte[] byteToReturn = new byte[1 + 1];
            int indexBitmapByte = (fieldParam - 1) / 8;
            int indexBitmapBit = (fieldParam - 1) % 8;

            if (fieldParam >= 64)  //Usa el segundo Bitmap
            {
                byteToReturn = AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_SecondaryBitmapKeyname].Content, 2, (indexBitmapByte - 8) * 2));
            }
            else //Usa el primer Bitmap
            {
                byteToReturn = AbstractTransactionFacade.GetHexaBytes(AbstractTransactionFacade.GetString(this[_PrimaryBitmapKeyname].Content, 2, indexBitmapByte * 2));
            }

            return (byteToReturn[0] & (byte)(((int)mask) >> indexBitmapBit)) > 0;
        }
        #endregion
    }
}
