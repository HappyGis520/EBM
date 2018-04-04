/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： Book.cs
 * * 功   能：  EllaBook中的书对象
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-04 11:16:47
 * * 修改记录： 
 * * 日期时间： 2018-04-04 11:16:47  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace EllaBookMaker.Model
{
    public class Book:ObservableObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(()=>Name);
            }
        }
    }
}
