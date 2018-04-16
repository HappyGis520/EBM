using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace EllaBookMaker.Model
{
    public  class Sprite:ObservableObject
    {
        #region 私有字段
        private string _Name;
        private EnumSpriteType _SpriteType;
        private Size _SpriteSize;
        private Point _AnchorPosition;
        private Point _SpritePosition;
        private double _ScaleForX;
        private double _ScaleForY;
        private double _SkewForX;
        private double _SkewForY;
        private double _Rotate;
        private double _Transparency;
        private Color _SpriteColor;
        private bool _IsCamera;
        private bool _IsAlphadetection;
        private string _SpriteLable;
        private bool _Relactive = false;
        private List<AnimationGroupBase> _AnimationGroupList;
        #endregion

        #region  公有属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => _Name; set => _Name = value; }
        /// <summary>
        /// 切片类别
        /// </summary>
        public EnumSpriteType SpriteType
        {
            get => _SpriteType;
            set
            {
                _SpriteType = value;
                RaisePropertyChanged(()=>SpriteType);
            }
        }

        /// <summary>
        /// 切片尺寸
        /// </summary>
        public Size SpriteSize
        {
            get => _SpriteSize;
            set
            {
                _SpriteSize = value;
                RaisePropertyChanged(()=>SpriteSize);
            }
        }


        /// <summary>
        /// 锚点位置
        /// </summary>
        public Point AnchorPosition
        {
            get => _AnchorPosition;
            set
            {
                _AnchorPosition = value; 
                RaisePropertyChanged(()=>AnchorPosition);
            }

        }

        /// <summary>
        /// 切片位置
        /// </summary>
        public Point SpritePosition
        {
            get => _SpritePosition;
            set
            {
                _SpritePosition = value; 
                RaisePropertyChanged(()=>SpritePosition);
            }
        }

        /// <summary>
        /// X缩放比例
        /// </summary>
        public double ScaleForX
        {
            get => _ScaleForX;
            set
            {
                _ScaleForX = value;
                RaisePropertyChanged(()=>ScaleForX);
            }
        }

        /// <summary>
        /// Y缩放比例
        /// </summary>
        public double ScaleForY
        {
            get => _ScaleForY;
            set
            {
                _ScaleForY = value;
                RaisePropertyChanged(()=>ScaleForY);
            }
        }

        /// <summary>
        /// X Skew
        /// </summary>
        public double SkewForX
        {
            get => _SkewForX;
            set
            {
                _SkewForX = value;
                RaisePropertyChanged(()=>SkewForX);
            }
        }

        /// <summary>
        /// Y Kew
        /// </summary>
        public double SkewForY
        {
            get => _SkewForY;
            set
            {
                _SkewForY = value;
                RaisePropertyChanged(()=>SkewForY);
            }
        }

        /// <summary>
        /// 旋转
        /// </summary>
        public double Rotate
        {
            get => _Rotate;
            set
            {
                _Rotate = value;
                RaisePropertyChanged(() => Rotate);
            }
        }

        /// <summary>
        /// 透明度
        /// </summary>
        public double Transparency
        {
            get => _Transparency;
            set
            {
                _Transparency = value;
                RaisePropertyChanged(()=>Transparency);
            }
        }

        /// <summary>
        /// 颜色 
        /// </summary>
        public Color SpriteColor
        {
            get => _SpriteColor;
            set
            {
                _SpriteColor = value;
                RaisePropertyChanged(()=>SpriteColor);
            }
        }

        /// <summary>
        /// 是否刺激活透明通道
        /// </summary>
        public bool IsAlphadetection
        {
            get => _IsAlphadetection;
            set
            {
                _IsAlphadetection = value; 
                RaisePropertyChanged(()=>IsAlphadetection);
            }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string SpriteLable
        {
            get => _SpriteLable;
            set
            {
                _SpriteLable = value;
                RaisePropertyChanged(()=>SpriteLable);
            }
        }

        /// <summary>
        /// 相对核心区激活
        /// </summary>
        public bool Relactive
        {
            get => _Relactive;
            set
            {
                _Relactive = value;
                RaisePropertyChanged(()=>Relactive);
            }
        }

        ///// <summary>
        ///// 是否为镜面
        ///// </summary>
        //public bool IsCamera { get => _IsCamera; set => _IsCamera = value; }
        /// <summary>
        /// 动画组集合
        /// </summary>
        public List<AnimationGroupBase> AnimationGroupList
        {
            get => _AnimationGroupList;
            set
            {
                _AnimationGroupList = value;
                RaisePropertyChanged(()=>AnimationGroupList);
            }
        }

        #endregion


        #region 公有方法

        

        #endregion
    }
}
