using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Beltran
{
    public class BeltranStructure :  AbstractTransactionParserStructure
    {
         #region Constructor
        public BeltranStructure(Nullable<AbstractTransactionParserStructureType> type,
                               string rootSection = "Beltran")
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
                //Debug.WriteLine(String.Format("Id={3} Keyname={0} Length={1} Descripcion={2}", f.Keyname, f.Length, f.Description,f.Id));
                BeltranField newField = new BeltranField()
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
    }
}
