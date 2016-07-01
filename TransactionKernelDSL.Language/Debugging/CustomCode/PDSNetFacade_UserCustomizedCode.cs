

    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Configuration;
    using System.Xml;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;

namespace PDS.Switch.PDSNet
{

    /// <summary>
    /// User-Customized Definitions for ransactional facade's solution PDSNet
    /// </summary>
    /// <remarks>
    /// Generated on 29/06/2016 11:40:17
    /// </remarks>
	public partial class PDSNetFacade : AbstractTransactionFacade
	{
        ///Herein should be declared every Const string, with a valid and unique definition within this facade.
        ///For instance:
        ///   internal const string ConstParameter1 = "ConstParameter1";

        #region User Customized Setup
        private void PDSNetFacadeUserCustomizedSetup()
        {
             //#region Example Setup
             //{
             //   int intDefaultValue = 0;
             //   if (Int32.TryParse((GetValue("PDSNet.General", "ExampleValue") as List<string>)[0], out intDefaultValue) == false)
             //       intDefaultValue = 0;
             //   _ExampleValue = intDefaultValue;
             //}
             //#endregion 
        }
        #endregion
        
        #region User Customized Start Engines Method
        private void PDSNetFacadeStartEngines_UC()
        {            
            ///Herein should be the implementation for all the those manual-start engines in every layer, in order to start them
            //TLX_XXXLayer_XXXEngine.Instance.Start();
        }
        #endregion

        #region User Customized Stop Engines Method
        private void PDSNetFacadeStopEngines_UC()
        {
            ///Herein should be the implementation for all the those manual-start engines in every layer, in order to stop them
            //TLX_XXXLayer_XXXEngine.Instance.Stop();
        }
        #endregion
           
     }
}
