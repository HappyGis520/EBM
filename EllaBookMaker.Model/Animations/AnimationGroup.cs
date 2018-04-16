using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using EllaBookMaker.Model;
using GalaSoft.MvvmLight;

namespace EllaBookMaker.Model
{
    /// <summary>
    /// 动画组基类
    /// </summary>
    [Serializable]
    public abstract class AnimationGroupBase:ObservableObject
    {
        private Sprite _ParentSprite;
        /// <summary>
        /// 父对象
        /// </summary>
        public Sprite Parent { get => _ParentSprite; set => _ParentSprite = value; }
        private string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            //set { _Name = value; }
        }

        protected EnumAnimationTriggerType _AnimationTriggerType;
        /// <summary>
        /// 动画触 发方式
        /// </summary>
        public EnumAnimationTriggerType AnimationTriggerType { get => _AnimationTriggerType; /*set => _AnimationTriggerType = value; */}
        /// <summary>
        /// 音频路径
        /// </summary>
        public string AudioFilePath { get => _AudioFilePath; set => _AudioFilePath = value; }


        protected string _AudioFilePath;

        public AnimationGroupBase(string Name)
        {
            this._Name = Name;
            this._AudioFilePath = AudioFilePath;
        }
        /// <summary>
        /// 所属切片的名称
        /// </summary>
        public string ParentSpriteName
        {
            get { return _ParentSprite.Name; }
        }
    }
    /// <summary>
    /// 自动播放动画组
    /// </summary>
    public class AutoAnimationGroup:AnimationGroupBase
    {
        private bool _EnableBreakPlay;
        private EnumAnimationPlayResumType _ResumPalyType;
        private string _StartTime;
        private string _EndTime;

        /// <summary>
        /// 是否允许中断播放
        /// </summary>
        public bool EnableBreakPlay
        {
            get => _EnableBreakPlay;
            set
            {
                _EnableBreakPlay = value;
                RaisePropertyChanged(()=>EnableBreakPlay);
            }
        }

        /// <summary>
        /// 播放恢复方式
        /// </summary>
        public EnumAnimationPlayResumType ResumPalyType { get => _ResumPalyType; set => _ResumPalyType = value; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get => _StartTime; set => _StartTime = value; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get => _EndTime; set => _EndTime = value; }
        public AutoAnimationGroup(string Name):base(Name)
        {
            this._AnimationTriggerType = EnumAnimationTriggerType.Auto;
            
        }




    }
    /// <summary>
    /// 可交互动画组
    /// </summary>
    public abstract class InteractiveAnimationGroup:AnimationGroupBase
    {
        private  bool _IsBuddyCaller = false;
        /// <summary>
        /// 是否为主关联，是：True,否：false
        /// </summary>
        public bool IsBuddyCaller { get => _IsBuddyCaller; set => _IsBuddyCaller = value; }
        private List<AnimationGroupBase> _BuddyAnimationGroup;
        /// <summary>
        /// 关联动画组集合
        /// </summary>
        public List<AnimationGroupBase> BuddyAnimationGroup { get => _BuddyAnimationGroup; set => _BuddyAnimationGroup = value; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name">动画组名称</param>
        /// <param name="IsBuddyCaller">是否为主关联动画组</param>
        public InteractiveAnimationGroup(string Name, bool IsBuddyCaller) :base(Name)
        {
            this._IsBuddyCaller = IsBuddyCaller;
        }
    }
    /// <summary>
    /// 点触动画组
    /// </summary>
    public class TouchAnimationGroup: InteractiveAnimationGroup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name">动画组名称</param>
        /// <param name="IsBuddyCaller">是否为主关联动画组</param>
        public TouchAnimationGroup(string Name, bool IsBuddyCaller) : base(Name, IsBuddyCaller)
        {
            
        }

    }
    /// <summary>
    /// 拖动触发动画组
    /// </summary>
    public class DragAnimationGroup:InteractiveAnimationGroup
    {
        private InteractionAninationTriggerCondition _TriggerCodition;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name">动画组名称</param>
        /// <param name="IsBuddyCaller">是否为主关联动画组</param>
        public DragAnimationGroup(string Name, bool IsBuddyCaller) :base(Name, IsBuddyCaller)
        {

        }
        /// <summary>
        /// 触发条件
        /// </summary>
        public InteractionAninationTriggerCondition TriggerCodition
        {
            get => _TriggerCodition;
            set
            {
                _TriggerCodition = value;
                RaisePropertyChanged(()=>TriggerCodition);
            }
        }
    }
    /// <summary>
    /// 滑动触发动画
    /// </summary>
    public class SlipAnimationGroup:InteractiveAnimationGroup
    {
        public SlipAnimationGroup(string Name, bool IsBuddyCaller) : base(Name,IsBuddyCaller)
        {

        }

    }
    /// <summary>
    /// 长按触发动画组
    /// </summary>
    public class HoldAnimationGroup : InteractiveAnimationGroup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="IsBuddyCaller">是否为主关联</param>
        public HoldAnimationGroup(string Name, bool IsBuddyCaller) :base(Name,IsBuddyCaller)
        {

        }
    }

}
