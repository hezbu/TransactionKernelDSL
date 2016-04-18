using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;
using System.Configuration;

namespace TransactionKernelDSL.Framework.Parser.Iso8583
{
    public sealed class Iso8583Structure : AbstractTransactionParserStructure
    {
        private string _BitmapKeyname = null;

        #region Constructor
        public Iso8583Structure(Nullable<AbstractTransactionParserStructureType> type,
                                string rootSection = "Iso8583",
                                string bitmapKeyname = "BITMAP") :
            base(rootSection, type)
        {
            _BitmapKeyname = bitmapKeyname;

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
                Iso8583Field newField = new Iso8583Field()
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
                Fields.Add(newField.Id, newField);
            }
            #endregion
        }
        #endregion


        #region Owned Methods
        public void TurnOnField(int fieldParam)
        {
            byte mask = 0x80;

            if (fieldParam < 1 || fieldParam > 128)
            {
                throw new ArgumentOutOfRangeException("Field number is out of range (" + fieldParam.ToString() + "vs 1 - 128)");
            }

            int indexBitmapByte = (fieldParam - 1) / 8;
            int indexBitmapBit = (fieldParam - 1) % 8;

            this[_BitmapKeyname].Content[indexBitmapByte] = (byte)(((int)this[_BitmapKeyname].Content[indexBitmapByte]) | (mask >> indexBitmapBit));

        }
        public void TurnOffField(int fieldParam)
        {
            byte mask = 0x80;

            if (fieldParam < 1 || fieldParam > 128)
            {
                throw new ArgumentOutOfRangeException("Field number is out of range (" + fieldParam.ToString() + "vs 1 - 128)");
            }

            int indexBitmapByte = (fieldParam - 1) / 8;
            int indexBitmapBit = (fieldParam - 1) % 8;

            this[_BitmapKeyname].Content[indexBitmapByte] = (byte)(((int)this[_BitmapKeyname].Content[indexBitmapByte]) & (~(mask >> indexBitmapBit)));

        }
        public bool IsFieldInBitmap(int fieldParam)
        {
            byte mask = 0x80;
            if (fieldParam < 1 || fieldParam > 128)
            {
                throw new ArgumentOutOfRangeException("Field number is out of range (" + fieldParam.ToString() + "vs 1 - 128)");
            }
            int indexBitmapByte = (fieldParam - 1) / 8;
            int indexBitmapBit = (fieldParam - 1) % 8;

            return ((this[_BitmapKeyname].Content[indexBitmapByte] & (mask >> indexBitmapBit)) == (mask >> indexBitmapBit));
        }
        #endregion
    }
}
