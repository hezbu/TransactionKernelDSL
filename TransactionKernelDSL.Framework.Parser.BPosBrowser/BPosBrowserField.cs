using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.BPosBrowser
{
    public class BPosBrowserField : AbstractTransactionParserField
    {
        public int CopyContentFrom(string src)
        {
            switch (this.Type)
            {
                default:             
                    return base.CopyContentFrom(AbstractTransactionFacade.GetBytes(src));              
            }
        }



        public string ToString(bool includeLen)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");

            switch (this.Type)
            {               
                default:
                    return AbstractTransactionFacade.GetString(this.Content, this.Content.Length - 1);
            }
        }

        public string ToBinString(bool includeLen)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");

            switch (this.Type)
            {                
                default:
                    return AbstractTransactionFacade.GetBinString(this.Content, this.Content.Length - 1);
            }
        }

        public override string ToString()
        {
            return this.ToString(true);
        }
    }
}
