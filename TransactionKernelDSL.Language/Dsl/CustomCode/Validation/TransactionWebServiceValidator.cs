using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.CSharp;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TransactionWebService
    {
        private static CSharpCodeProvider csharp = new CSharpCodeProvider();

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateWebServiceClassNameAsValidIdentifier(ValidationContext context)
        {
            if (!csharp.IsValidIdentifier(WebServiceClassName.Replace(" ", "")))
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.WebServiceClassNameInvalidNameError, Name), "Validation", this);
        }

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateWebServiceClassNameEmptyness(ValidationContext context)
        {
            if (String.IsNullOrWhiteSpace(WebServiceClassName.Replace(" ", "")))
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.WebServiceClassNameEmptynessError, Name), "Validation", this);
        }

    }
}
