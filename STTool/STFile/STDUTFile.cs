using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STDUTFile : STFileBase
    {
        public STDUTFile(string fullName):base(fullName)
        {
            FileType = STFileType.DUT;
            Parse();
            ParseSTElement();
        }

        public void Parse()
        {
            XmlNode pNode = xmlDoc.SelectSingleNode("/TcPlcObject/DUT");
            Name = pNode.Attributes["Name"].Value;

            XmlNode node = xmlDoc.SelectSingleNode("/TcPlcObject/DUT/Declaration");
            if (node != null)
            {
                DeclarationText = node.InnerText;
            }
            else
            {
                DeclarationText = "";
            }
        }
    }
}
