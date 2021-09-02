using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STTool.STFile.STFileBase;

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
            ENUM,
            STRUCT,
            METHOD,
            ACTION,
            UNKNOWN
        }

        public STFileType FileType;

        public STElement(STFileType fileType)
        {
            FileType = fileType;
        }
    }
}
