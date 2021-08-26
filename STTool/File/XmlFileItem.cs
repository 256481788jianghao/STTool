using STTool.STFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.File
{
    class XmlFileItem
    {

        public enum FileType
        {
            GVL,
            POU,
            Interface,
            UnKnown
        }
        public string FullName { get; set; }
        public XmlDocument xmlDoc { get; set; } = new XmlDocument();

        public Object stFile = null;

        public XmlFileItem(string fullName)
        {
            FullName = fullName;
            xmlDoc.Load(fullName);

            if(GetFileType() == FileType.GVL)
            {
                stFile = new STGVLFile(xmlDoc);
            }
            else if (GetFileType() == FileType.POU)
            {
                stFile = new STPOUFile(xmlDoc);
            }
            else if (GetFileType() == FileType.Interface)
            {
                stFile = new STInterfaceFile(xmlDoc);
            }
        }


        public FileType GetFileType()
        {
            string ExtensionName = System.IO.Path.GetExtension(FullName);
            if (ExtensionName.Contains("TcGVL"))
            {
                return FileType.GVL;
            }
            else if (ExtensionName.Contains("TcPOU"))
            {
                return FileType.POU;
            }
            else if (ExtensionName.Contains("TcIO"))
            {
                return FileType.Interface;
            }
            else
            {
                return FileType.UnKnown;
            }
        }
    }
}
