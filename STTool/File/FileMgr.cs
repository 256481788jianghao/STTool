using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTool.File
{
    class FileMgr
    {
        string m_rootpath;
        public List<FileTreeViewItem> FileTreeViewList = new List<FileTreeViewItem>();
        public List<string> FullNameFileList = new List<string>();
        public FileMgr(string rootpath)
        {
            this.m_rootpath = rootpath;
        }

        public void Parse()
        {
            DirectoryInfo TheFolder = new DirectoryInfo(m_rootpath);
            FileTreeViewItem item = new FileTreeViewItem();
            item.Name = m_rootpath;
            ParseInternal(item, TheFolder);
            FileTreeViewList.Add(item);
        }

        void ParseInternal(FileTreeViewItem parent,DirectoryInfo Folder)
        {
            DirectoryInfo[] dInfo = Folder.GetDirectories();
            if(dInfo.Length == 0)
            {
                FileInfo[] fInfo = Folder.GetFiles();
                if(fInfo.Length == 0)
                {
                    return;
                }
                else
                {
                    foreach(FileInfo info in fInfo)
                    {
                        FileTreeViewItem item = new FileTreeViewItem();
                        item.Name = info.Name;
                        parent.Children.Add(item);
                        string fullName = info.FullName;
                        if(fullName.Contains("TcPOU") || fullName.Contains("TcDUT"))
                        {
                            FullNameFileList.Add(info.FullName);
                        }
                    }
                }
            }
            else
            {
                foreach(DirectoryInfo info in dInfo)
                {
                    FileTreeViewItem item = new FileTreeViewItem();
                    item.Name = info.Name;
                    item.IsFolder = true;
                    parent.Children.Add(item);
                    ParseInternal(item, info);
                }
            }
        }
    }
}
