using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
  
    class STGVLFile:STFile
    {
        public STGVLFile(XmlDocument xmlDoc)
        {
            this.xmlDoc = xmlDoc;
            STModeType = STType.GVL;
            Parse();
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
