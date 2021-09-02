using STTool.STParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace STTool.STFile
{
    class STFileBase:STParseFile
    {
        public enum STFileType
        {
            POU,
            DUT,
            INTERFACE,
            GVL,
            Method,
            UNKNOWN
        }


        protected XmlDocument xmlDoc;
        public string Name;
        public string FullName;

       

        public STFileBase(string fullName)
        {
            FullName = fullName;
            xmlDoc = new XmlDocument();
            xmlDoc.Load(FullName);
        }
    }
}
