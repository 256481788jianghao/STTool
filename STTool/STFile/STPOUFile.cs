using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STPOUFile : STFileBase
    {
        public List<STMethod> MethodList = new List<STMethod>();
        public STPOUFile(string fullName) : base(fullName)
        {
            FileType = STFileType.POU;
            Parse(); 
        }

        public void Parse()
        {
            XmlNode pNode = xmlDoc.SelectSingleNode("/TcPlcObject/POU");
            Name = pNode.Attributes["Name"].Value;

            XmlNode node = xmlDoc.SelectSingleNode("/TcPlcObject/POU/Declaration");
            if (node != null)
            {
                DeclarationText = node.InnerText;
            }
            else
            {
                DeclarationText = "";
            }

            XmlNode node2 = xmlDoc.SelectSingleNode("/TcPlcObject/POU/Implementation/ST");
            if (node2 != null)
            {
                ImplementationText = node2.InnerText;
            }
            else
            {
                ImplementationText = "";
            }

            XmlNodeList methodList = xmlDoc.SelectNodes("/TcPlcObject/POU/Method");
            foreach(XmlNode item in methodList)
            {
                MethodList.Add(new STMethod(Name,item));
            }

            XmlNodeList actionList = xmlDoc.SelectNodes("/TcPlcObject/POU/Action");
            foreach (XmlNode item in actionList)
            {
                MethodList.Add(new STMethod(Name,item));
            }
        }
    }
}
