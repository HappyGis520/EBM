/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： EnumAnimation.cs
 * * 功   能：  动画相关枚举类
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-12 17:48:05
 * * 修改记录： 
 * * 日期时间： 2018-04-12 17:48:05  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaBookMaker.Model
{
    /// <summary>
    /// 动画触发类型
    /// </summary>
    [Serializable]
    public enum EnumAnimationTriggerType
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,
        /// <summary>
        /// 自动
        /// </summary>
        Auto = 0,
        /// <summary>
        /// 点触
        /// </summary>
        Touch = 1,
        /// <summary>
        /// 滑动
        /// </summary>
        Slip = 2,
        /// <summary>
        /// 拖拽
        /// </summary>
        Drag = 3,
        /// <summary>
        /// 长按
        /// </summary>
        Hold = 4
    }
    /// <summary>
    /// 动画类型
    /// </summary>
    public enum EnumAnimationStyle
    {
        平移动画 = 0,
        旋转动画 = 1,
        缩放动画 = 2,
        闪烁动画 = 4,
        色调动画 = 5,
        渐变动画 = 6,
        贝塞尔曲线 = 7,
        帧动画 = 10,
        动态图片 = 11,
        弹动动画 = 12,
        扭曲动画 = 13,
        延迟动画 = 14,
        单轴旋转 = 15,
        重置动画 = 16,
        圆周动画 = 17,
        点击替换 = 18,
        延迟平移组合 = 19,
        延迟贝塞尔组合 = 20,
        骨骼动画 = 21
    }
    /// <summary>
    ///动画恢复播放方式
    /// </summary>
    public enum EnumAnimationPlayResumType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow = -1,
        /// <summary>
        /// 重新开始
        /// </summary>
        Replay = 0,
        /// <summary>
        /// 交互终止处播放
        /// </summary>
        Resum = 1,
        /// <summary>
        /// 自定义播放位置
        /// </summary>
        Custom = 2
    }
    /// <summary>
    /// 交互动画组动画触发条件
    /// </summary>
    public enum EnumInteractionAninationTriggerCondition
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = -1,
        /// <summary>
        /// 点吻合
        /// </summary>
        IdenticalPoint = 0,
        /// <summary>
        /// 范围吻合
        /// </summary>
        IdenticalBound =1



    }
}
