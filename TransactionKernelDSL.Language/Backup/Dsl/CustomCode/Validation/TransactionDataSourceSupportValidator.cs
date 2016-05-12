using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using TransactionKernelDSL.Framework.Language.CustomCode.Validation;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TransactionDataSourceSupport       
    {
        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateValidOutputEngine(ValidationContext context)
        {
            foreach (OutputTransactionEngine oe in this.OutputTransactionEngines)
            {
                switch (oe.Type)
                {
                    case OutputEngineType.TcpFunneledOutputEngine:
                    case OutputEngineType.FunneledOutputEngine:
                        break;
                    default:
                        context.LogError(String.Format(ValidationResources.DataSourceSupportInvalidEngineTypeError, oe.Name), "Validation", this);
                        break;
                }
            }
            
        }
    }
}
