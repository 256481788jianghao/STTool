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
        public List<XmlFileItem> XmlFileList = new List<XmlFileItem>();
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

        public XmlFileItem FindXmlItemByName(string name)
        {
            if(XmlFileList.Count > 0)
            {
                XmlFileItem ret = null;
                foreach(XmlFileItem item in XmlFileList)
                {
                    string fullName = item.FullName;
                    string FileName = System.IO.Path.GetFileName(fullName);
                    if (FileName.Contains(name))
                    {
                        ret = item;
                        break;
                    }
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

        void ParseInternal(FileTreeViewItem parent,DirectoryInfo Folder)
        {
            
            FileInfo[] fInfo = Folder.GetFiles();
            if (fInfo.Length > 0)
            {
                foreach(FileInfo info in fInfo)
                {
                    FileTreeViewItem item = new FileTreeViewItem();
                    item.Name = info.Name;
                    parent.Children.Add(item);
                    string fullName = info.FullName;
                    if(fullName.Contains("TcPOU") || fullName.Contains("TcDUT") || fullName.Contains("TcGVL") || fullName.Contains("TcIO"))
                    {
                        XmlFileList.Add(new XmlFileItem(info.FullName));
                    }
                }
            }
            DirectoryInfo[] dInfo = Folder.GetDirectories();
            if (dInfo.Length > 0)
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
