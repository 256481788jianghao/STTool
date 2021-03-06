using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STPOUFile : STFile
    {
        public string ImplementationText;
        public List<STMethod> MethodList = new List<STMethod>();
        public STPOUFile(XmlDocument xmlDoc)
        {
            this.xmlDoc = xmlDoc;
            Parse(); 
        }

        public void ParseModeType(string Dstr)
        {
            string[] lines = Dstr.Split('\n');
            string firstLine = lines[0];
            string[] firstLineSp = firstLine.Split(' ');
            if (firstLineSp[0].Contains("PROGRAM"))
            {
                STModeType = STType.PROGRAM;
            }
            else if (firstLineSp[0].Contains("FUNCTION_BLOCK"))
            {
                STModeType = STType.FUNCTIONBLOCK;
            }
            else if (firstLineSp[0].Contains("FUNCTION"))
            {
                STModeType = STType.FUNCTION;
            }
        }

        public void Parse()
        {
            XmlNode pNode = xmlDoc.SelectSingleNode("/TcPlcObject/POU");
            Name = pNode.Attributes["Name"].Value;

            XmlNode node = xmlDoc.SelectSingleNode("/TcPlcObject/POU/Declaration");
            if (node != null)
            {
                DeclarationText = node.InnerText;
                ParseModeType(DeclarationText);
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
