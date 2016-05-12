using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using EnvDTE80;
using System.Diagnostics;

namespace TransactionKernelDSL.Framework.Language
{
    public partial class TransactionHandlerShape : NodeShape
    {
        public override void OnDoubleClick(DiagramPointEventArgs e)
        {
            // Get an instance of the currently running Visual Studio IDE.
            string file = null;

            try
            {
                EnvDTE80.DTE2 dte10;
                dte10 = DTEHelper.GetCurrent();

                TransactionHandler modelElement = (this.ModelElement as TransactionHandler);
                file = @"TL" + modelElement.TransactionLayer.Level + "_" + modelElement.TransactionLayer.Name.Replace(" ", "") + "_" + modelElement.Name.Replace(" ", "") + "_Handler.cs";
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
