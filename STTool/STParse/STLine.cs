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

        public STLine(string linestr)
        {
            linestr = linestr.Trim();
            string[] sp = linestr.Split(' ');
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
