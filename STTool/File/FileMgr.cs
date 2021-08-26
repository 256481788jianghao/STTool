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

        public STMethod FindMethodByName(string name,string parentName)
        {
            if (XmlFileList.Count > 0)
            {
                STMethod ret = null;
                foreach (XmlFileItem item in XmlFileList)
                {
                    if (item.GetFileType() == XmlFileItem.FileType.POU)
                    {
                        STPOUFile pouFile = (STPOUFile)item.stFile;
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
                    if (item.GetFileType() == XmlFileItem.FileType.Interface)
                    {
                        STInterfaceFile InterfaceFile = (STInterfaceFile)item.stFile;
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
            if(XmlFileList.Count > 0)
            {
                foreach(XmlFileItem xmlitem in XmlFileList)
                {
                    if (xmlitem.GetFileType() == XmlFileItem.FileType.POU)
                    {
                        STPOUFile stPOUFile = (STPOUFile)xmlitem.stFile;
                        if (stPOUFile.Name != Name && (stPOUFile.DeclarationText.Contains(Name) || stPOUFile.ImplementationText.Contains(Name)))
                        {
                            retList.Add(new YinYongListViewItem(stPOUFile.Name,xmlitem.FullName));
                        }
                    }
                }
            }
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
                    
                    string fullName = info.FullName;
                    if(fullName.Contains("TcPOU") || fullName.Contains("TcDUT") || fullName.Contains("TcGVL") || fullName.Contains("TcIO"))
                    {
                        XmlFileItem xmlItem = new XmlFileItem(info.FullName);
                        XmlFileList.Add(xmlItem);
                        if(xmlItem.GetFileType() == XmlFileItem.FileType.POU)
                        {
                            STPOUFile stPouFile = (STPOUFile)xmlItem.stFile;
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
                        if(xmlItem.GetFileType() == XmlFileItem.FileType.Interface)
                        {
                            STInterfaceFile stInterfaceFile = (STInterfaceFile)xmlItem.stFile;
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
