using STTool.File;
using STTool.STFile;
using STTool.STParse;
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
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_DicSelect_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            string testpath = "D:\\project\\project_Snake\\Snake_arm_robot219042\\branch_jh\\SnakeArmRobotProject_GuangZhou\\SnakeArmRobot";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShowLabel_Dic.ValueStr = testpath;// dialog.SelectedPath;
                FolderPath = testpath;// dialog.SelectedPath;

                GVL.gFileMgr = new FileMgr(FolderPath);
                GVL.gFileMgr.Parse();
                TreeView_F.ItemsSource = GVL.gFileMgr.FileTreeViewList;
            }
        }

        private void Button_Parse_Click(object sender, RoutedEventArgs e)
        {
            //m_FileMgr = new FileMgr(FolderPath);
            //m_FileMgr.Parse();
            //TreeView_F.ItemsSource = m_FileMgr.FileTreeViewList;
        }

        private List<YinYongListViewItem> FindYinYongByName(string namestr)
        {
            List<YinYongListViewItem> ret = new List<YinYongListViewItem>();
            foreach(STFileBase stfile in GVL.gFileMgr.STFileList)
            {
                if(stfile.FileType == STFileBase.STFileType.POU)
                {
                    if(stfile.DeclarationElement.VARElements.Count > 0)
                    {
                        foreach(STVARElement el in stfile.DeclarationElement.VARElements)
                        {
                            if(el.Variables.Count > 0)
                            {
                                foreach(STVARIABLEElement v in el.Variables)
                                {
                                    if(v.TypeName == namestr)
                                    {
                                        ret.Add(new YinYongListViewItem(stfile.Name, stfile.FullName, false)); ;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ret;
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
                        STMethod method = GVL.gFileMgr.FindMethodByName(item.Name,item.MethodParent);
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
                        STFileBase stFileBase = GVL.gFileMgr.FindSTFileByFullName(item.FullName);
                        if (stFileBase != null)
                        {
                            TextBlock_D.Text = stFileBase.DeclarationText;
                            TextBlock_I.Text = stFileBase.ImplementationText;

                            if(stFileBase.FileType == STFileBase.STFileType.POU)
                            {
                                ListView_YinYong.ItemsSource = FindYinYongByName(stFileBase.Name);
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

        private void ListView_YinYong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
