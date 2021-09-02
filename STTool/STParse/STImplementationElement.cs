using STTool.STFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.STParse
{
    class STImplementationElement : STElement
    {
        public STImplementationElement(STFileBase.STFileType fileType) : base(fileType)
        {
        }

        public void Parse(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {

            }
        }
    }
}
