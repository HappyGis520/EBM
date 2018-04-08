/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： EnumExportType.cs
 * * 功   能：  书发布时选择的模式
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-08 14:16:26
 * * 修改记录： 
 * * 日期时间： 2018-04-08 14:16:26  修改人：王建军  提取
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaBookMaker.Model
{
    public enum EnumExportType
    {
        发布所有版本 = 1,
        按页码帆布 = 2,
        导出书籍 = 3,
        合并书籍 = 4
}
}
