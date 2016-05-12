using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.BPosBrowser
{
    public class BPosBrowserStructure : AbstractTransactionParserStructure
    {
        public BPosBrowserStructure(Nullable<AbstractTransactionParserStructureType> type,
                                string rootSection = "BPosBrowserParser"
                                ) :
            base(rootSection, type)
        {          

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
    }
}
