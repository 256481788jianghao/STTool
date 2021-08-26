using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STInterfaceFile : STFile
    {
        public List<STMethod> MethodList = new List<STMethod>();
        public STInterfaceFile(XmlDocument xmlDoc)
        {
            this.xmlDoc = xmlDoc;
            STModeType = STType.INTERFACE;
            Parse();
        }

        public void Parse()
        {
            XmlNode pNode = xmlDoc.SelectSingleNode("/TcPlcObject/Itf");
            Name = pNode.Attributes["Name"].Value;

            XmlNodeList methodList = xmlDoc.SelectNodes("/TcPlcObject/Itf/Method");
            foreach (XmlNode item in methodList)
            {
                MethodList.Add(new STMethod(Name,item));
            }
        }
    }
}
