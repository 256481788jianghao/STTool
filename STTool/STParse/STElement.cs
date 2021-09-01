using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.STParse
{
    class STElement
    {
        public enum ElementType
        {
            FUNCTION,
            FUNCTIONBLOCK,
            PROGRAM,
            INTERFACE,
            GVL,
            METHOD,
            ACTION,
            UNKNOWN
        }

        public STDeclarationElement Declaration = new STDeclarationElement();
        public STImplementationElement Implementation = new STImplementationElement();

        public void Parse(string DContent,string IContent)
        {
            Declaration.Parse(DContent);
            Implementation.Parse(IContent);
        }
    }
}
