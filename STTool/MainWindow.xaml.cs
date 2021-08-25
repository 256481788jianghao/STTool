using STTool.File;
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
            }
        }

        private void Button_Parse_Click(object sender, RoutedEventArgs e)
        {
            m_FileMgr = new FileMgr(FolderPath);
            m_FileMgr.Parse();
            TreeView_F.ItemsSource = m_FileMgr.FileTreeViewList;
        }
    }
}
