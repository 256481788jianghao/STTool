using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STMethod
    {
        XmlNode xnode;
        public string DeclarationText;
        public string ImplementationText;
        public string name;
        public string parentName;

        public STMethod(string pName,XmlNode node)
        {
            this.parentName = pName;
            this.xnode = node;
            Parse();
        }

        void Parse()
        {
            name = xnode.Attributes["Name"].Value;

            XmlNode node = xnode.SelectSingleNode("Declaration");
            if (node != null)
            {
                DeclarationText = node.InnerText;
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
