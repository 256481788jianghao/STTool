using STTool.STFile;
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
        public List<STFileBase> STFileList = new List<STFileBase>();
        public FileMgr(string rootpath)
        {
            this.m_rootpath = rootpath;
        }

        public void Parse()
        {
            DirectoryInfo TheFolder = new DirectoryInfo(m_rootpath);
            FileTreeViewItem item = new FileTreeViewItem();
            item.Name = System.IO.Path.GetFileName(m_rootpath);
            item.FullName = m_rootpath;
            ParseInternal(item, TheFolder);
            FileTreeViewList.Add(item);
        }

        public STFileBase FindSTFileByFullName(string fullname)
        {
            STFileBase ret = null;
            foreach(STFileBase item in STFileList)
            {
                if(item.FullName == fullname)
                {
                    return item;
                }
            }
            return ret;
        }

        public STMethod FindMethodByName(string name,string parentName)
        {
            if (STFileList.Count > 0)
            {
                STMethod ret = null;
                foreach (STFileBase item in STFileList)
                {
                    if (item.FileType == STFileBase.STFileType.POU)
                    {
                        STPOUFile pouFile = (STPOUFile)item;
                        if (pouFile.Name == parentName && pouFile.MethodList.Count > 0)
                        {
                            foreach (STMethod method in pouFile.MethodList)
                            {
                                if (method.Name == name)
                                {
                                    return method;
                                }
                            }
                        }
                    }
                    if (item.FileType == STFileBase.STFileType.INTERFACE)
                    {
                        STInterfaceFile InterfaceFile = (STInterfaceFile)item;
                        if (InterfaceFile.Name == parentName && InterfaceFile.MethodList.Count > 0)
                        {
                            foreach (STMethod method in InterfaceFile.MethodList)
                            {
                                if (method.Name == name)
                                {
                                    return method;
                                }
                            }
                        }
                    }
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

        public List<YinYongListViewItem> FindYinYongList(string Name)
        {
            List<YinYongListViewItem> retList = new List<YinYongListViewItem>();
            
            return retList;
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
                    item.FullName = info.FullName;
                    
                    string fullName = info.FullName;
                    if(fullName.Contains("TcPOU") || fullName.Contains("TcDUT") || fullName.Contains("TcGVL") || fullName.Contains("TcIO"))
                    {
                        STFileBase stFileBase = null;
                        if (fullName.Contains("TcPOU"))
                        {
                            stFileBase = new STPOUFile(info.FullName);
                        }
                        if (fullName.Contains("TcDUT"))
                        {
                            stFileBase = new STDUTFile(info.FullName);
                        }
                        if (fullName.Contains("TcGVL"))
                        {
                            stFileBase = new STGVLFile(info.FullName);
                        }
                        if (fullName.Contains("TcIO"))
                        {
                            stFileBase = new STInterfaceFile(info.FullName);
                        }
                        STFileList.Add(stFileBase);
                        if(stFileBase.FileType == STFileBase.STFileType.POU)
                        {
                            STPOUFile stPouFile = (STPOUFile)stFileBase;
                            if(stPouFile.MethodList.Count > 0)
                            {
                                foreach (STMethod method in stPouFile.MethodList)
                                {
                                    FileTreeViewItem subitem = new FileTreeViewItem();
                                    subitem.Name = method.Name;
                                    subitem.IsMethod = true;
                                    subitem.MethodParent = stPouFile.Name;
                                    item.Children.Add(subitem);
                                }
                            }
                        }
                        if(stFileBase.FileType == STFileBase.STFileType.INTERFACE)
                        {
                            STInterfaceFile stInterfaceFile = (STInterfaceFile)stFileBase;
                            if (stInterfaceFile.MethodList.Count > 0)
                            {
                                foreach (STMethod method in stInterfaceFile.MethodList)
                                {
                                    FileTreeViewItem subitem = new FileTreeViewItem();
                                    subitem.Name = method.Name;
                                    subitem.IsMethod = true;
                                    subitem.MethodParent = stInterfaceFile.Name;
                                    item.Children.Add(subitem);
                                }
                            }
                        }
                    }

                    parent.Children.Add(item);
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
