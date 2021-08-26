using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static STTool.STFile.STFile;

namespace STTool.STFile
{
    class STMethod:STFile
    {
        XmlNode xnode;
        
        public string ImplementationText;
        
        public string parentName;
        

        public STMethod(string pName,XmlNode node)
        {
            this.parentName = pName;
            this.xnode = node;
            Parse();
        }

        void Parse()
        {
            Name = xnode.Attributes["Name"].Value;

            XmlNode node = xnode.SelectSingleNode("Declaration");
            if (node != null)
            {
                DeclarationText = node.InnerText;
                STModeType = STType.METHOD;
            }
            else
            {
                DeclarationText = "";
            }

            XmlNode node2 = xnode.SelectSingleNode("Implementation/ST");
            if (node2 != null)
            {
                ImplementationText = node2.InnerText;
            }
            else
            {
                ImplementationText = "";
            }
        }
    }
}
