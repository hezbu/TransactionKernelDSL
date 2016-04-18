using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using TransactionKernelDSL.Framework.Language.CustomCode.Validation;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TransactionHandlerReferencesForwarderTransactionHandlers
    {
        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateSequenceOrderMoreOrEqualThanZero(ValidationContext context)
        {
            if (this.SequenceOrder < 0)
            {
                context.LogError(String.Format(ValidationResources.SequenceOrderMoreOrEqualThanZeroError, new object[] { this.SourceForwardingTransactionHandler.Name, this.TargetForwardingTransactionHandler.Name }), "Validation", this);
            }
        }
    }
}
