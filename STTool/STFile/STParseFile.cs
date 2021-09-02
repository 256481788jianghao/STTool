using STTool.STParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STTool.STFile.STFileBase;

namespace STTool.STFile
{
    class STParseFile
    {
        public string DeclarationText;
        public string ImplementationText;

        public STDeclarationElement DeclarationElement;
        public STImplementationElement ImplementationElement;

        public STFileType FileType = STFileType.UNKNOWN;

        public  void ParseSTElement()
        {
            DeclarationElement = new STParse.STDeclarationElement(FileType);
            ImplementationElement = new STParse.STImplementationElement(FileType);
            DeclarationElement.Parse(DeclarationText);
            ImplementationElement.Parse(ImplementationText);
        }
    }
}
