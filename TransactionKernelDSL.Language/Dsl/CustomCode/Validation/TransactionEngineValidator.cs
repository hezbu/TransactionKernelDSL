using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.CSharp;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TransactionEngine   
    {
        private static CSharpCodeProvider csharp = new CSharpCodeProvider();

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateNameAsValidIdentifier(ValidationContext context)
        {
            if (!csharp.IsValidIdentifier(Name.Replace(" ", "")))
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.EngineInvalidNameError, Name), "Validation", this);
        }
    }
}
