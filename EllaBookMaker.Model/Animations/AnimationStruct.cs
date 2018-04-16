using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaBookMaker.Model
{
    public struct InteractionAninationTriggerCondition
    {
        /// <summary>
        /// 条件类别
        /// </summary>
        public EnumInteractionAninationTriggerCondition Condition;
        /// <summary>
        /// 中心点
        /// </summary>
        public Point CenterPoint;
        /// <summary>
        /// 最大覆盖半径
        /// </summary>
        public Int64 MaxRadius;
        /// <summary>
        /// 最大覆盖范围
        /// </summary>
        public Size MaxRegion;
        /// <summary>
        /// 是否可以触发
        /// </summary>
        /// <param name="CurPoint"></param>
        /// <returns></returns>
        public bool CanTrigger(Point CurPoint)
        {
            return true;
        }
    }
}
