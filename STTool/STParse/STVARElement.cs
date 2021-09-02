using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.STParse
{
    class STVARElement
    {
        public enum STVARType
        {
            GLOBAL,
            INPUT,
            OUTPUT,
            VAL,
            UNKNOWN
        }
        public STVARType VARType = STVARType.UNKNOWN;
        public List<STLine> Lines = new List<STLine>();

        public STVARElement(STVARType vALType)
        {
            VARType = vALType;
        }
    }
}
