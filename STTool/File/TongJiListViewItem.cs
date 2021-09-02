using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.File
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class TongJiListViewItem
    {
        public string Name { get; set; }
        public string FullName { get; set; }

        public TongJiListViewItem(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
        }
    }
}
