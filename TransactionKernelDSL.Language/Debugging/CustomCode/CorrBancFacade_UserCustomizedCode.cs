

    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Configuration;
    using System.Xml;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;

namespace PDS.Switch.CorrBanc
{

    /// <summary>
    /// User-Customized Definitions for ransactional facade's solution CorrBanc
    /// </summary>
    /// <remarks>
    /// Generated on 17/4/2016 11:19:20
    /// </remarks>
	public partial class CorrBancFacade : AbstractTransactionFacade
	{
        ///Herein should be declared every Const string, with a valid and unique definition within this facade.
        ///For instance:
        ///   internal const string ConstParameter1 = "ConstParameter1";

        #region User Customized Setup
        private void CorrBancFacadeUserCustomizedSetup()
        {
             //#region Example Setup
             //{
             //   int intDefaultValue = 0;
             //   if (Int32.TryParse((GetValue("CorrBanc.General", "ExampleValue") as List<string>)[0], out intDefaultValue) == false)
             //       intDefaultValue = 0;
             //   _ExampleValue = intDefaultValue;
             //}
             //#endregion 
        }
        #endregion
        
        #region User Customized Start Engines Method
        private void CorrBancFacadeStartEngines_UC()
        {            
            ///Herein should be the implementation for all the those manual-start engines in every layer, in order to start them
            //TLX_XXXLayer_XXXEngine.Instance.Start();
        }
        #endregion

        #region User Customized Stop Engines Method
        private void CorrBancFacadeStopEngines_UC()
        {
            ///Herein should be the implementation for all the those manual-start engines in every layer, in order to stop them
            //TLX_XXXLayer_XXXEngine.Instance.Stop();
        }
        #endregion
           
     }
}
