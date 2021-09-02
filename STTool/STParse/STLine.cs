using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.STParse
{
    class STLine
    {
        public List<string> Words = null;
        public string Content = null;

        public STLine(string linestr)
        {
            int index = linestr.IndexOf("//");
            if(index > 0)
            {
                linestr = linestr.Substring(0, index);
            }
            
            Content = linestr.Trim();
            string[] sp = Content.Split(' ');
            Words = new List<string>();
            foreach (string item in sp)
            {
                if (item.StartsWith("{") && item.Length > 1)
                {
                    Words.Add("{");
                    Words.Add(item.Substring(1));
                }
                else if (item.EndsWith("}") && item.Length > 1)
                {
                    Words.Add(item.Substring(0, item.Length-1));
                    Words.Add("}");
                }
                else
                {
                    Words.Add(item);
                }
            }
        }
    }
}
