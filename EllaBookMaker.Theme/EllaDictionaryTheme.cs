/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： EllaDictionaryTheme.cs
 * * 功   能：  扩展WPF皮肤
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-11 16:42:06
 * * 修改记录： 
 * * 日期时间： 2018-04-11 16:42:06  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EllaBookMaker.Theme.Default
{
    /// <summary>
    /// 皮肤资源ResourceDictionary
    /// </summary>
    public abstract class EllaDictionaryTheme : EllaTheme
    {
    public EllaDictionaryTheme()
    {
    }

    public EllaDictionaryTheme( ResourceDictionary themeResourceDictionary )
    {
      this.ThemeResourceDictionary = themeResourceDictionary;
    }

    public ResourceDictionary ThemeResourceDictionary
    {
      get;
      private set;
    }

    public override Uri GetResourceUri()
    {
      return null;
    }
  }
}
