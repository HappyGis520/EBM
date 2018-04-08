using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EllaBookMaker.Utility;

namespace EllaBookMaker.EllaControl
{

    public partial class  Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
            newBook.Click += aa;
            ////文件
            //newBook.Click += BookOptionEvents.newBook_Click;
            //openBook.Click += BookOptionEvents.openBook_Click;
            //saveBook.Click += BookOptionEvents.saveBook_Click;
            //saveAsBook.Click += BookOptionEvents.saveAsBook_Click;
            //AppendBook.Click += BookOptionEvents.AppendBook_Click;
            ////发布
            //complieOnePage_cmi.Click += PreviewOptionEvents.complieOnePage_cmi_Click;
            //complie_cmi.Click += ExportOptionEvents.complie_cmi_Click;
            //complieZM_cmi.Click += ExportOptionEvents.complieZM_cmi_Click;
            //exportJson_cmi3.Click += ExportOptionEvents.exportJson_cmi3_Click;
            //exportProductFile.Click += ExportOptionEvents.exportProductFile_Click;
            //calculatePublishSize.Click += ExportOptionEvents.calculatePublishSize_Click;
            //exportSettings.Click += ExportOptionEvents.exportSettings_Click;
            //bookupload.Click += ExportOptionEvents.bookupload_Click;
            //bookview.Click += PreviewOptionEvents.bookview_Click;
            ////窗口
            //resetPanel.Click += OtherOptionEvents.resetPanel_Click;
            ////帮助
            //frameeditor.Click += OtherOptionEvents.frameeditor_Click;
            //feedBack.Click += OtherOptionEvents.feedBack_Click;
            //compressPng.Click += OtherOptionEvents.compressPng_Click;

        }

        private void aa(object sender, RoutedEventArgs e)
        {
            JLog.Instance.Error("dfsafsafsafas");

        }

        private void openTempDir_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Process.Start("Explorer.exe ", "/select," + System.IO.Path.Combine(ExportHelper.TempPath, "ellabook") );
          
        }
    }
}
