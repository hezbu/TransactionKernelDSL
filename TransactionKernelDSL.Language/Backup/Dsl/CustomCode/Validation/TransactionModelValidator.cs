using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.CSharp;
using TransactionKernelDSL.Framework.Language.CustomCode.Validation;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TransactionModel
    {
        private static CSharpCodeProvider csharp = new CSharpCodeProvider();

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateNameAsValidIdentifier(ValidationContext context)
        {
            if (!csharp.IsValidIdentifier(Name.Replace(" ","")))
                context.LogError(String.Format(ValidationResources.ModelInvalidNameError, Name), "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateModelName(ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(Name))
                context.LogError(ValidationResources.ModelNameError, "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateModelInstanceId(ValidationContext context)
        {
            if (InstanceId <= 0)
                context.LogError(ValidationResources.ModelInstanceIdError, "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateModelSatelliteInstancesId(ValidationContext context)
        {
            if (String.IsNullOrEmpty(SatelliteInstances) == false)
            {
                foreach (string s in SatelliteInstances.Split(new char[] { '|' }))
                {
                    int instanceTest = 0;
                    if (Int32.TryParse(s, out instanceTest) == false)
                    {
                        context.LogError(String.Format(ValidationResources.SatelliteInstancesError, s), "Validation", this);
                    }
                }
                
            }
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateModelNamespace(ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(Namespace))
                context.LogError(ValidationResources.ModelNamespaceError, "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateModelConnectionString(ValidationContext context)
        {
            if(String.IsNullOrEmpty(DatabaseServerInstance) == true)            
                context.LogWarning(ValidationResources.ModelConnectionStringWarning, "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateModelOnlyOneFunneledEngine(ValidationContext context)
        {
            var list = from layer in this.TransactionLayers
                       let outputEngines = layer.OutputTransactionEngines
                       from outputEngine in outputEngines
                       where (
                                (outputEngine.Type == OutputEngineType.TcpFunneledOutputEngine || outputEngine.Type == OutputEngineType.FunneledOutputEngine) &&
                                (outputEngine.TransactionDataSourceSupport != null) &&
                                (outputEngine.TransactionDataSourceSupport.SupportType == DataSourceSupportType.SQLServerSupport)
                             )
                       select outputEngine;
            if (list.ToList().Count > 1)
            {
                context.LogError(ValidationResources.ModelOnlyOneFunneledEngineError, "Validation", this);
            }

        }
        
    }
}
