using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
  
    class STGVLFile: STFileBase
    {
        public STGVLFile(string fullName) : base(fullName)
        {
            FileType = STFileType.GVL;
            Parse();
            ParseSTElement();
        }

        public void Parse()
        {
            XmlNode pNode = xmlDoc.SelectSingleNode("/TcPlcObject/GVL");
            Name = pNode.Attributes["Name"].Value;

            XmlNode node = xmlDoc.SelectSingleNode("/TcPlcObject/GVL/Declaration");
            if (node != null)
            {
                DeclarationText =  node.InnerText;
            }
            else
            {
                DeclarationText = "";
            }
        }
    }
}
