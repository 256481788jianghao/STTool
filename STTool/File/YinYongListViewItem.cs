using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.File
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class YinYongListViewItem
    {
        public string FBName { get; set; }
        public string FullName { get; set; }

        public bool IsMethod { get; set; }

        public YinYongListViewItem(string fBName,string fullName,bool isMethod)
        {
            FBName = fBName;
            FullName = fullName;
            IsMethod = isMethod;
        }
    }
}
