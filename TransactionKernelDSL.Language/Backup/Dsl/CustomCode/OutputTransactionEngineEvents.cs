using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace TransactionKernelDSL.Framework.Language
{
    public partial class OutputTransactionEngineShape : CompartmentShape
    {
        public override void OnDoubleClick(DiagramPointEventArgs e)
        {
            // Get an instance of the currently running Visual Studio IDE.
            string file = null;

            try
            {
                EnvDTE80.DTE2 dte10;
                dte10 = DTEHelper.GetCurrent();

                OutputTransactionEngine modelElement = (this.ModelElement as OutputTransactionEngine);
                file = @"TL" + modelElement.TransactionLayer.Level + "_" + modelElement.TransactionLayer.Name.Replace(" ", "") + "_" + modelElement.Name.Replace(" ", "") + "_Engine.cs";
                base.OnDoubleClick(e);
                dte10.ExecuteCommand("File.OpenFile", file);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Can't open file " + file + " Ex: " + ex.Message);
            }

        }
    }
}
