using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace TransactionKernelDSL.Framework.V1
{
    public class AbstractTransactionParserSection : ConfigurationSection
    {
         [ConfigurationProperty("DefaultRequestFields",
           IsDefaultCollection = false)]
        public FieldsCollection DefaultRequestFields
        {
            get
            {
                FieldsCollection defaultRequestFields = (FieldsCollection)base["DefaultRequestFields"];
                return defaultRequestFields;
            }
        }

        [ConfigurationProperty("DefaultResponseFields",
          IsDefaultCollection = false)]
        public FieldsCollection DefaultResponseFields
        {
            get
            {
                FieldsCollection defaultResponseFields = (FieldsCollection)base["DefaultResponseFields"];
                return defaultResponseFields;
            }
        }

        [ConfigurationProperty("Transactions",
           IsDefaultCollection = false)]
        public TransactionsCollection Transactions
        {
            get
            {
                TransactionsCollection transactions = (TransactionsCollection)base["Transactions"];
                return transactions;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// v1.0.1.0 (HZ) (22/07/2013) -> Se agrega el campo no obligatorio DefaultValue, para indicar el valro por defecto con el que debe ser cargado cuando se instancia la estructura.
    /// </remarks>
    public class FieldElement : ConfigurationElement
    {
        [ConfigurationProperty("id",
          IsRequired = true,
          IsKey = true)]
        public int Id
        {
            get
            {
                return (int)this["id"];
            }
        }

        [ConfigurationProperty("keyName",
          IsRequired = true)]
        public string Keyname
        {
            get
            {
                return (string)this["keyName"];
            }
        }

        [ConfigurationProperty("type",
         IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
        }

        [ConfigurationProperty("length",
      IsRequired = true)]
        public int Length
        {
            get
            {
                return (int)this["length"];
            }
        }

        [ConfigurationProperty("descr",
         IsRequired = false)]
        public string Description
        {
            get
            {
                return (string)this["descr"];
            }
        }

        [ConfigurationProperty("IsTransactionIdentifier",
       IsRequired = false
       )]
        public bool IsTransactionIdentifier
        {
            get
            {
                return Convert.ToBoolean(this["IsTransactionIdentifier"]);
            }
        }

        [ConfigurationProperty("IsRequired",
      IsRequired = false
      )]
        public bool IsRequired
        {
            get
            {
                return Convert.ToBoolean(this["IsRequired"]);
            }
        }

        [ConfigurationProperty("DefaultValue",
         IsRequired = false
         )]        
        public string DefaultValue
        {
            get
            {
                return (string)this["DefaultValue"];
            }
        }


        [ConfigurationProperty("subfields",
         IsDefaultCollection = false, IsRequired = false)]
        public SubFieldCollection Subfields
        {
            get
            {
                return (SubFieldCollection)this["subfields"];
            }
        }
    }
    public class FieldsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FieldElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FieldElement)element).Id + ((FieldElement)element).Description + ((FieldElement)element).Type;
        }

        public FieldElement this[int index]
        {
            get
            {
                return (FieldElement)BaseGet(index);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "field"; }
        }
    }

    public class SubFieldElement : ConfigurationElement
    {
        [ConfigurationProperty("id",
          IsRequired = true,
          IsKey = true)]
        public int Id
        {
            get
            {
                return (int)this["id"];
            }
        }

        [ConfigurationProperty("keyName",
          IsRequired = true)]
        public string Keyname
        {
            get
            {
                return (string)this["keyName"];
            }
        }

        [ConfigurationProperty("type",
         IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
        }

        [ConfigurationProperty("descr",
         IsRequired = true)]
        public string Description
        {
            get
            {
                return (string)this["descr"];
            }
        }

        [ConfigurationProperty("length",
        IsRequired = false)]
        public int Length
        {
            get
            {
                return (int)this["length"];
            }
        }

        [ConfigurationProperty("offset",
          IsRequired = true)]
        public int Offset
        {
            get
            {
                return (int)this["offset"];
            }
        }


    }
    public class SubFieldCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SubFieldElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SubFieldElement)element).Id + ((SubFieldElement)element).Keyname + ((SubFieldElement)element).Description + ((SubFieldElement)element).Type;
        }

        public SubFieldElement this[int index]
        {
            get
            {
                return (SubFieldElement)BaseGet(index);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "subfield"; }
        }
    }

    public class TransactionsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TransactionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TransactionElement)element).Name;
        }

        public TransactionElement this[int index]
        {
            get
            {
                return (TransactionElement)BaseGet(index);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "Transaction"; }
        }

    }
    public class TransactionElement : ConfigurationElement
    {
        [ConfigurationProperty("name",
           IsRequired = true,
           IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("id",
          IsRequired = true)]
        public string Id
        {
            get
            {
                return (string)this["id"];
            }
            set
            {
                this["id"] = value;
            }
        }

        [ConfigurationProperty("responseId",
         IsRequired = false)]
        public string ResponseId
        {
            get
            {
                return (string)this["responseId"];
            }
            set
            {
                this["responseId"] = value;
            }
        }

        [ConfigurationProperty("errorId",
          IsRequired = false)]
        public string ErrorId
        {
            get
            {
                return (string)this["errorId"];
            }
            set
            {
                this["errorId"] = value;
            }
        }

        [ConfigurationProperty("Request",
          IsDefaultCollection = false)]
        public FieldsCollection RequirementFields
        {
            get
            {
                return (FieldsCollection)this["Request"];
            }
        }

        [ConfigurationProperty("Response",
         IsDefaultCollection = false)]
        public FieldsCollection ResponseFields
        {
            get
            {
                return (FieldsCollection)this["Response"];
            }
        }

        [ConfigurationProperty("ErrorRequirementFields",
        IsDefaultCollection = false)]
        public FieldsCollection ErrorRequirementFields
        {
            get
            {
                return (FieldsCollection)this["ErrorRequirementFields"];
            }
        }

        [ConfigurationProperty("ErrorResponse",
        IsDefaultCollection = false)]
        public FieldsCollection ErrorResponseFields
        {
            get
            {
                return (FieldsCollection)this["ErrorResponse"];
            }
        }

    }

    
}
