using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaBookMaker.Model
{



    /// <summary>
    /// 动画接口
    /// </summary>
    public interface IAnimation
    {
        Object[] GetAnimationObjects();
        void Dispose();
    }
    public abstract  class AnimationBase
    {


    }
}
