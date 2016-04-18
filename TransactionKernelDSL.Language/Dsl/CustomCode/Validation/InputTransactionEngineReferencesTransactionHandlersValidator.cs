using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using TransactionKernelDSL.Framework.Language.CustomCode.Validation;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class InputTransactionEngineReferencesTransactionHandlers     
    {
        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateSourceAndTargetWithinSameLayer(ValidationContext context)
        {
            if (this.TransactionHandler.TransactionLayer.Level != this.InputTransactionEngine.TransactionLayer.Level)
            {
                context.LogError(String.Format(ValidationResources.SourceAndTargetWithinSameLayerError, new object[] { this.TransactionHandler.Name, this.InputTransactionEngine.Name }), "Validation", this);
            }
        }
    }
}
