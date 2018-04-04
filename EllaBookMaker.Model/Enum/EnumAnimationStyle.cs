using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaBookMaker.Model
{
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
}
