using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace TransactionKernelDSL.Framework.Language
{
    public partial class TransactionEnvironmentSQLServerVariableShape : NodeShape
    {
        /// <summary>
        /// Keeps the size of the expanded bounds of the shape
        /// </summary>
        protected RectangleD ExpandedBounds;

        
        /// <summary>
        /// When we collapse the shape, we keep its current bound and reduce its size
        /// </summary>
        protected override void Collapse()
        {
            base.Collapse();
            this.ExpandedBounds = this.Bounds;
            this.Bounds = this.AbsoluteBounds;
            this.AbsoluteBounds = new RectangleD(this.Location, new SizeD(1.5, 0.3));            
        }

        /// <summary>
        /// When we expand the shape, we restore the expanded bounds
        /// </summary>
        protected override void Expand()
        {
            base.Expand();
            this.Bounds = this.ExpandedBounds;
        }
       
    }
}
