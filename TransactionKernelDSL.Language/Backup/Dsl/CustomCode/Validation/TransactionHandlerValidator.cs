using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.CSharp;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TransactionHandler
    {
        private static CSharpCodeProvider csharp = new CSharpCodeProvider();

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateNameAsValidIdentifier(ValidationContext context)
        {
            if (!csharp.IsValidIdentifier(Name.Replace(" ", "")))
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.HandlerInvalidNameError, Name), "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateHandlerTransactionId(ValidationContext context)
        {
            if (String.IsNullOrWhiteSpace(TransactionId))
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.HandlerTransactionIdError, Name), "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateMaintenanceHandler(ValidationContext context)
        {
            if (MaintenanceTransactionHandler == this)
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.HandlerMaintenanceError, Name), "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateForwardingHandler(ValidationContext context)
        {
            foreach (TransactionHandler h in ForwarderTransactionHandlers)
            {
                if (h == this)
                    context.LogError(String.Format(CustomCode.Validation.ValidationResources.HandlerForwardingError, Name), "Validation", this);
            }
        }
    }
}
