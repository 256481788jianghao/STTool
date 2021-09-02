using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.STParse
{
    class STVARIABLEElement
    {
        public enum STVARIABLETYPE
        {
            SINGLE,
            ARRAY,
            UNKNOWN
        }
        string Content;

        public STVARIABLETYPE VariableType = STVARIABLETYPE.UNKNOWN;
        public string Name;
        public string TypeName;
        public string DefaultValue = "";

        public STVARIABLEElement(string content)
        {
            Content = content;
            string BeforeContent = Content.Replace(";","");
            if (Content.Contains(":="))
            {
                int index = Content.IndexOf(":=");
                BeforeContent = Content.Substring(0, index).Trim();
                DefaultValue = Content.Substring(index + 2, Content.Length - index - 2 - 1).Trim();
            }

            int bIndex = BeforeContent.IndexOf(":");
            Name = BeforeContent.Substring(0, bIndex).Trim();
            string TypeStr = BeforeContent.Substring(bIndex + 1, BeforeContent.Length - bIndex - 1).Trim();
            if (TypeStr.Contains("ARRAY"))
            {
                string[] sp = TypeStr.Split(' ');
                TypeName = sp.Last();
                VariableType = STVARIABLETYPE.ARRAY;
            }
            else
            {
                TypeName = TypeStr;
                VariableType = STVARIABLETYPE.SINGLE;
            }
        }
    }
}
