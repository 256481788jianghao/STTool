using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STFile
    {
        public enum STType
        {
            STRUCT,
            ENUM,
            GVL,
            FUNCTION,
            FUNCTIONBLOCK,
            PROGRAM,
            METHOD,
            ACTION,
            INTERFACE,
            UNKNOWN,
        }

        protected XmlDocument xmlDoc;

        public string Name;
        public string DeclarationText;
        public STType STModeType = STType.UNKNOWN;
    }
}
