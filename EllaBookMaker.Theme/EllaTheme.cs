/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： EllaTheme.cs
 * * 功   能：  扩展WPF皮肤 
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-11 16:39:33
 * * 修改记录： 
 * * 日期时间： 2018-04-11 16:39:33  修改人：王建军  创建
 * *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EllaBookMaker.Theme.Default
{
    /// <summary>
    /// 界面皮肤
    /// </summary>
    public abstract class EllaTheme : DependencyObject
    {
        public EllaTheme()
        {

        }

        public abstract Uri GetResourceUri();


    }
}
