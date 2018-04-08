/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： PreviewPagesCountCheck.xaml.cs
 * * 功   能：  发布时版本终端选择窗体
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-08 13:08:32
 * * 修改记录： 
 * * 日期时间： 2018-04-08 13:08:32  修改人：王建军  引入 /编辑
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EllaBookMaker.Windows
{
    /// <summary>
    /// PreviewPagesCountCheck.xaml 的交互逻辑
    /// </summary>
    public partial class PreviewPagesCountCheck : Window
    {
        public PreviewPagesCountCheck()
        {
            InitializeComponent();
            InitClientItems();
        }
        /// <summary>
        /// 初始化终端类型
        /// </summary>
        private void InitClientItems()
        {
            int count = 0;// TotalStats.GetCurrentStats().CurrentBook.PageCount;
            for (int i = 0; i < count; i++)
            {
                pageCount.Items.Add(i + 1);
            }

            isInited = true;
            pageCount.SelectedIndex = count >= 3 ? 2 : count - 1;
        }

        private bool isInited = false;
        public int previewCount { get; set; }

        public bool IsPreview = false;
        public bool SaveDir = false;
        public bool cancle = false;
        private void pageCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInited) return;
            previewCount = pageCount.SelectedIndex + 1;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void exportPreview_Click(object sender, RoutedEventArgs e)
        {
            IsPreview =  false;
            SaveDir = saveDir.IsChecked == true ? true : false;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            cancle = true;
            this.Close();
        }
    }
}
