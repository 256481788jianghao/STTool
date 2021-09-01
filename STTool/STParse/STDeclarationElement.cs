using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STTool.STParse.STElement;

namespace STTool.STParse
{
    class STDeclarationElement
    {
        public string Name;
        public ElementType Type = ElementType.UNKNOWN;

        List<STLine> m_Lines = new List<STLine>();
        public void Parse(string content)
        {
            if(content.Length > 0)
            {
                string[] temp_lines = content.Split('\n');
                foreach(string item in temp_lines)
                {
                    m_Lines.Add(new STLine(item));
                }
            }
        }
    }
}
