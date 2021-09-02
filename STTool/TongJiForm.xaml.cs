using STTool.File;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static STTool.STParse.STElement;

namespace STTool
{
    /// <summary>
    /// TongJiForm.xaml 的交互逻辑
    /// </summary>
    public partial class TongJiForm : Window
    {
        public TongJiForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_TongJiType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(GVL.gFileMgr.STFileList.Count > 0)
            {
                ElementType et = ElementType.UNKNOWN;
                switch (ComboBox_TongJiType.SelectedIndex)
                {
                    case 0: { et = ElementType.PROGRAM;break; }
                    case 1: { et = ElementType.FUNCTIONBLOCK; break; }
                    case 2: { et = ElementType.FUNCTION; break; }
                    case 3: { et = ElementType.INTERFACE; break; }
                    case 4: { et = ElementType.GVL; break; }
                    case 5: { et = ElementType.ENUM; break; }
                    case 6: { et = ElementType.STRUCT; break; }
                    default:break;
                }
                ListView_TongJi.ItemsSource = FindByType(et);
            }
        }

        List<TongJiListViewItem> FindByType(ElementType eType)
        {
            List<TongJiListViewItem> ret = new List<TongJiListViewItem>();
            foreach(STFileBase stFile in GVL.gFileMgr.STFileList)
            {
                if(stFile.DeclarationElement.ElType == eType)
                {
                    ret.Add(new TongJiListViewItem(stFile.Name,stFile.FullName));
                }
            }
            return ret;
        }
    }
}
