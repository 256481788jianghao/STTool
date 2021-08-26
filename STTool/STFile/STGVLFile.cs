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
            Parse();
        }

        public void Parse()
        {
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
