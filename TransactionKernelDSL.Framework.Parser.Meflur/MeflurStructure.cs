using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;
using System.Configuration;
using System.Diagnostics;

namespace TransactionKernelDSL.Framework.Parser.Meflur
{
    public class MeflurStructure : AbstractTransactionParserStructure
    {
        #region Constructor
        public MeflurStructure(Nullable<AbstractTransactionParserStructureType> type,
                               string rootSection = "Meflur")
            : base(rootSection, type)
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
                 //  Debug.WriteLine("Request, DefaultRequestFields will be used");
                    defaultFields = defaultLayout.DefaultRequestFields;
                    break;
                #endregion
                #region Default Response Fields
                case AbstractTransactionParserStructureType.Response:
                  //  Debug.WriteLine("Response, DefaultResponseFields will be used");
                    defaultFields = defaultLayout.DefaultResponseFields;
                    break;
                #endregion
                default: throw new ApplicationException("Structure Type " + type + "not found on " + rootSection + " structure");
            }
            #endregion
            #region Adding Default Fields
            foreach (FieldElement f in defaultFields)
            {
                MeflurField newField = new MeflurField()
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
        public override object GetOperationId()
        {
            string result = String.Empty;
            foreach (MeflurField f in Fields.Values)
            {
                if (f.IsTransactionIdentifier == true) result += f.Content;
            }
            return result;
        }

        public override byte[] GetField(string id)
        {
            return AbstractTransactionFacade.GetBytes(((MeflurField)Fields[Convert.ToInt32(id)]).Content);
        }

        public int ActiveDynamicFields
        {
            get
            {
                int activeDynamicFieldsFound = 0;

                foreach (MeflurField f in this.Fields.Values)
                {
                    if (f.IsRequired == false && f.HasContent == true) activeDynamicFieldsFound++;
                }

                return activeDynamicFieldsFound;
            }
        }
        #endregion
    }
}
