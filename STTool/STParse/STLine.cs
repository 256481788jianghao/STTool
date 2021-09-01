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
            linestr = linestr.Substring(0, linestr.Length);
            string[] sp = linestr.Split(' ');
            Words = new List<string>(sp);
        }
    }
}
