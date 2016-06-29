using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Configuration;
using log4net;

namespace TransactionKernelDSL.Framework.V1
{
 public abstract class AbstractTransactionParserStructure
 {
  #region Member Fields
  private ILog _Log = null;
  [XmlIgnore()]
  protected Nullable<AbstractTransactionParserStructureType> _StructureType = null;

  [XmlIgnore()]
  protected Dictionary<int, AbstractTransactionParserField> _Fields;
  protected string _RootSection;
  #endregion

  #region Member Properties
  public virtual Nullable<AbstractTransactionParserStructureType> StructureType
  {
   get { return _StructureType; }
  }
  public virtual Dictionary<int, AbstractTransactionParserField> Fields
  {
   get
   {
    return _Fields;
   }
  }
  #endregion

  public string Logger
  {
   set
   {
    _Log = LogManager.GetLogger(value);
   }
  }

  #region Constructor
  public AbstractTransactionParserStructure(string rootSection, Nullable<AbstractTransactionParserStructureType> type = null)
  {
   _RootSection = rootSection;
   _Log = LogManager.GetLogger("MainLogger");
   _StructureType = type;
   _Fields = new Dictionary<int, AbstractTransactionParserField>();
  }
  #endregion

  #region Virtual Methods
  public virtual AbstractTransactionParserField this[int id]
  {
   get
   {
    return _Fields[id];
   }
  }
  public virtual AbstractTransactionParserField this[string keyname]
  {
   get
   {
    foreach (AbstractTransactionParserField field in _Fields.Values)
    {
     if (field.Keyname == keyname) return field;
     if (!String.IsNullOrEmpty(field.Alias))
     {
      if (field.Alias == keyname) return field;
     }
    }

    return null;
   }
  }
  public virtual object GetOperationId()
  {
   string result = String.Empty;
   foreach (AbstractTransactionParserField f in _Fields.Values)
   {
    if (f.IsTransactionIdentifier == true)
    {
     switch (f.Type)
     {
      case AbstractTransactionParserFieldType.LLVAR:
       result += AbstractTransactionFacade.GetString(f.Content, f.Content.Length - 3,2);
       break;
      default:
       result += AbstractTransactionFacade.GetString(f.Content, f.Content.Length - 1);
       break;
     }
     
    }
   }
   return result;
  }
  public virtual byte[] GetField(string id)
  {
   return _Fields[Convert.ToInt32(id)].Content;
  }
  public virtual string GetBio(string rootSection, string example = "N/A")
  {
   string biography = "<table border=\"0\" width=\"100%\" style=\"white-space: pre-wrap;\"><tr><td align=\"left\"></td></tr>";
   Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
   AbstractTransactionParserSection defaultLayout = config.GetSection(rootSection) as AbstractTransactionParserSection;

   if (defaultLayout == null) throw new ApplicationException("ConfigSection " + rootSection + " not found on app.config!");
   foreach (TransactionElement t in defaultLayout.Transactions)
   {
    if (t.Id == this.GetOperationId().ToString() && this._StructureType == AbstractTransactionParserStructureType.Request)
    {
     biography += "<tr><td style=\"color:green;\"><h3>Requerimiento " + t.Name + " </h3></td></tr>";
     biography += "<tr><td><b>Ejemplo de Trama:</b> " + example + "</td></tr>";
     biography += "<tr><td>" + GetBioFields(t.RequirementFields) + "</td></tr>";
     break;
    }
    else if (t.Id == this.GetOperationId().ToString() && this._StructureType == AbstractTransactionParserStructureType.Response)
    {
     biography += "<tr><td style=\"color:green;\"><h3>Respuesta " + t.Name + " </h3></td></tr>";
     biography += "<tr><td><b>Ejemplo de Trama:</b> " + example + "</td></tr>";
     biography += "<tr><td>" + GetBioFields(t.ResponseFields) + "</td></tr>";
     break;
    }
    else if (t.ResponseId == this.GetOperationId().ToString() && this._StructureType == AbstractTransactionParserStructureType.Response)
    {
     biography += "<tr><td style=\"color:green;\"><h3>Respuesta " + t.Name + " </h3></td></tr>";
     biography += "<tr><td><b>Ejemplo de Trama:</b> " + example + "</td></tr>";
     biography += "<tr><td>" + GetBioFields(t.ResponseFields) + "</td></tr>";
     break;
    }
    else if (t.ErrorId == this.GetOperationId().ToString() && this._StructureType == AbstractTransactionParserStructureType.Request)
    {
     biography += "<tr><td style=\"color:green;\"><h3>Requerimiento (CON ERROR) " + t.Name + " </h3></td></tr>";
     biography += "<tr><td><b>Ejemplo de Trama:</b> " + example + "</td></tr>";
     biography += "<tr><td>" + GetBioFields(t.ErrorRequirementFields) + "</td></tr>";
     break;
    }
    else if (t.ErrorId == this.GetOperationId().ToString() && this._StructureType == AbstractTransactionParserStructureType.Response)
    {
     biography += "<tr><td style=\"color:green;\"><h3>Respuesta (CON ERROR) " + t.Name + " </h3></td></tr>";
     biography += "<tr><td><b>Ejemplo de Trama:</b> " + example + "</td></tr>";
     biography += "<tr><td>" + GetBioFields(t.ErrorResponseFields) + "</td></tr>";
     break;
    }
   }


   return biography + "</table>";
  }
  #endregion

  #region Owned Methods
  private string GetBioFields(FieldsCollection fields)
  {
   string result = "<table border=\"1\" width=\"100%\" style=\"white-space: pre-wrap;\">" +
                   "<tr>" +
                   "<td align=\"center\" style=\"background-color:Grey;\"><b>Id</b></td>" +
                   "<td align=\"center\" style=\"background-color:Grey;\"><b>Type</b></td>" +
                   "<td align=\"center\" style=\"background-color:Grey;\"><b>Description</b></td>" +
                   "<td align=\"center\" style=\"background-color:Grey;\" width=\"70%\"><b>Example</b></td>" +
                   "</tr>";


   foreach (FieldElement fld in fields)
   {
    if (_Fields[fld.Id].HasValue == true)
    {
     result += "<tr>";
     result += "<td rowspan=\"" + (fld.Subfields.Count + 1).ToString() + "\">" + fld.Id + "</td>";
     result += "<td rowspan=\"" + (fld.Subfields.Count + 1).ToString() + "\">" + fld.Type + "</td>";
     result += "<td rowspan=\"" + (fld.Subfields.Count + 1).ToString() + "\">" + fld.Description + "</td>";
     result += "<td>\"" + _Fields[fld.Id].ToString() + "\"</td>";


     result += "</tr>";

     foreach (SubFieldElement sf in fld.Subfields)
     {
      int sfLength = sf.Length;
      if (sfLength > 0) //HZ: Has a real value defined on XML file
      {
       if (_Fields[fld.Id].ToString().Length < (sf.Offset + sf.Length)) sfLength = _Fields[fld.Id].ToString().Length - sf.Offset;

       result += "<tr>";
       result += "<td><b>[" + sf.Id + "-" + sf.Description + "</b> " + sf.Type + ", Desde " + sf.Offset.ToString() + " hasta " + (sfLength + sf.Offset).ToString() + "]:\"" + _Fields[fld.Id].ToString().Substring(sf.Offset, sfLength) + "\"</td>";
       result += "</tr>";
      }
      else //In contrast, use *.Length - Offset
      {
       result += "<tr>";
       result += "<td><b>[" + sf.Id + "-" + sf.Description + "</b> " + sf.Type + ", Desde " + sf.Offset.ToString() + " hasta " + (_Fields[fld.Id].ToString().Length - sf.Offset).ToString() + "]:\"" + _Fields[fld.Id].ToString().Substring(sf.Offset) + "\"</td>";
       result += "</tr>";
      }
     }
    }
   }

   return result + "</table>";
  }
  #endregion
 }
}
