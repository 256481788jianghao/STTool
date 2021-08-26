﻿using STTool.File;
using STTool.STFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        string FolderPath = "";
        FileMgr m_FileMgr;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_DicSelect_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShowLabel_Dic.ValueStr = dialog.SelectedPath;
                FolderPath = dialog.SelectedPath;

                m_FileMgr = new FileMgr(FolderPath);
                m_FileMgr.Parse();
                TreeView_F.ItemsSource = m_FileMgr.FileTreeViewList;
            }
        }

        private void Button_Parse_Click(object sender, RoutedEventArgs e)
        {
            //m_FileMgr = new FileMgr(FolderPath);
            //m_FileMgr.Parse();
            //TreeView_F.ItemsSource = m_FileMgr.FileTreeViewList;
        }

        private void TreeView_F_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FileTreeViewItem item = (FileTreeViewItem)(e.NewValue);
            if(item != null)
            {
                TextBlock_D.Text = "";
                TextBlock_I.Text = "";
                if (!item.IsFolder)
                {
                    if (item.IsMethod)
                    {
                        STMethod method = m_FileMgr.FindMethodByName(item.Name,item.MethodParent);
                        if(method != null)
                        {
                            TextBlock_D.Text = method.DeclarationText;
                            TextBlock_I.Text = method.ImplementationText;
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("not find " + item.Name);
                        }
                    }
                    else
                    {
                        XmlFileItem xmlitem = m_FileMgr.FindXmlItemByName(item.Name);
                        if (xmlitem != null)
                        {
                            switch (xmlitem.GetFileType())
                            {
                                case XmlFileItem.FileType.GVL:
                                    {
                                        STGVLFile gvlFile = (STGVLFile)xmlitem.stFile;
                                        TextBlock_D.Text = gvlFile.DeclarationText;
                                        break;
                                    }
                                case XmlFileItem.FileType.POU:
                                    {
                                        STPOUFile pouFile = (STPOUFile)xmlitem.stFile;
                                        TextBlock_D.Text = pouFile.DeclarationText;
                                        TextBlock_I.Text = pouFile.ImplementationText;
                                        break;
                                    }
                                case XmlFileItem.FileType.DUT:
                                    {
                                        STDUTFile dutFile = (STDUTFile)xmlitem.stFile;
                                        TextBlock_D.Text = dutFile.DeclarationText;
                                        break;
                                    }
                                default: { return; }
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("not find " + item.Name);
                        }
                    }
                }
            }
        }
    }
}
