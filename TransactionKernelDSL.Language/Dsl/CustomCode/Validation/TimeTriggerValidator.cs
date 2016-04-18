using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.CSharp;

namespace TransactionKernelDSL.Framework.Language
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TimeTrigger       
    {
        private static CSharpCodeProvider csharp = new CSharpCodeProvider();

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateNameAsValidIdentifier(ValidationContext context)
        {
            if (!csharp.IsValidIdentifier(Name.Replace(" ", "")))
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.TimeTriggerInvalidNameError, Name), "Validation", this);
        }       

        [ValidationMethod(ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateInputTransactionEngineType(ValidationContext context)
        {
            if(this.InputTransactionEngine == null)
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.TimeTriggerHasNotInputEngineError, Name), "Validation", this);
            
            else if (this.InputTransactionEngine.Type != InputEngineType.TimeTriggeredInputEngine)
                context.LogError(String.Format(CustomCode.Validation.ValidationResources.TimeTriggerEngineTypeInvalidError, Name), "Validation", this);
        }
        //
    }
}
