using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.File
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class FileTreeViewItem
    {
        public string Name { get; set; }
        public List<FileTreeViewItem> Children { get; set; } = new List<FileTreeViewItem>();
        public bool IsFolder { get; set; } = false;
        public bool IsMethod { get; set; } = false;
        public string MethodParent { get; set; }
    }
}
