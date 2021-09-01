using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STFileBase
    {
        public enum STFileType
        {
            POU,
            DUT,
            INTERFACE,
            GVL,
            UNKNOWN
        }


        protected XmlDocument xmlDoc;

        public string Name;
        public string DeclarationText;
        public string ImplementationText;
        public STFileType FileType = STFileType.UNKNOWN;
        public string FullName;

        public STFileBase(string fullName)
        {
            FullName = fullName;
            xmlDoc = new XmlDocument();
            xmlDoc.Load(FullName);
        }
    }
}
