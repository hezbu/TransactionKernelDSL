using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.CSharp;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TransactionLayer    
    {
        private static CSharpCodeProvider csharp = new CSharpCodeProvider();

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateNameAsValidIdentifier(ValidationContext context)
        {
            if (!csharp.IsValidIdentifier(Name.Replace(" ", "")))
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.LayerInvalidNameError, Name), "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateLayerLevel(ValidationContext context)
        {
            if (Level < 0)
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.LayerLevelError, Name), "Validation", this);
        }
    }
}
