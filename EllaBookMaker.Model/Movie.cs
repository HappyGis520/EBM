using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EllaBookMaker.Model
{
    /// <summary>
    /// 视频
    /// </summary>
    [Serializable]
    public class Movie
    {
        public Movie() { }
        /// <summary>
        /// 视频名称
        /// </summary>
        private string _VideoName = string.Empty;
        public string VideoName { get => _VideoName; set => _VideoName = value; }
        /// <summary>
        /// 视频源文件路径
        /// </summary>
        private string _VideoSourcePath = string.Empty;
        /// <summary>
        /// 视频源文件路径
        /// </summary>
        public string VideoSourcePath
        {
            get { return this._VideoSourcePath; }
            set
            {
                this._VideoSourcePath = value;
                if (File.Exists(value))
                    _VideoName = System.IO.Path.GetFileName(value);
            }
        }
        /// <summary>
        /// 适配模式
        /// </summary>
        private EnumScalingMode _ScalingMode;
        /// <summary>
        /// 适配模式
        /// </summary>
        public EnumScalingMode ScalingMode { get => _ScalingMode; set => _ScalingMode = value; }
        private EnumControlStyle _ControlStyle;
        /// <summary>
        /// 播放器类型
        /// </summary>
        public EnumControlStyle ControlStyle { get => _ControlStyle; set => _ControlStyle = value; }
        /// <summary>
        /// 视频源类型
        /// </summary>
        public EnumMovieSourceType movieSourceType;
        public string movieSourceTypeProperty { get { return movieSourceType.ToString(); } }


        #region 暂时不使用-wjj
        ///// <summary>
        ///// 视频参数
        ///// </summary>
        //public Frame frame;
        //public string frameProperty
        //{
        //    get
        //    {
        //        if (frame != null)
        //        {
        //            string str = "{\"x\":\"" + JsonExportHelper.ConvertToAppX(TotalStats.GetCurrentStats().rate, frame.X, parentSprite.Width, parentSprite.CorePoint.X, TotalStats.GetCurrentStats().cutWidth).ToString() + "\",\"y\":\"" +
        //                JsonExportHelper.ConvertToAppY(TotalStats.GetCurrentStats().rate, frame.Y, parentSprite.Height, parentSprite.CorePoint.Y).ToString() +
        //                "\",\"width\":\"" + frame.Width * TotalStats.GetCurrentStats().rate + "\",\"height\":\"" + frame.Height * TotalStats.GetCurrentStats().rate + "\"}";
        //            return str;
        //        }
        //        return "";
        //    }
        //}




        //public Movie(Sprite sprite, string path)
        //{
        //    frame = new Frame(this);
        //    frame.Width = sprite.Width;
        //    frame.Height = sprite.Height;
        //    parentSprite = sprite;
        //    frame.X = sprite.X;
        //    frame.Y = sprite.Y;
        //    _ScalingMode = ScalingMode.MPMovieScalingModeNone;
        //    _ControlStyle = ControlStyle.MPMovieControlStyleNone;
        //    movieSourceType = MovieSourceType.MPMovieSourceTypeStreaming;
        //    VideoSourcePath = path;
        //    VideoName = System.IO.Path.GetFileName(path);
        //}
        //public Sprite parentSprite;

        //public Movie Clone(Sprite sprite)
        //{
        //    Movie movie = new Movie();
        //    movie.frame = new Frame(movie);
        //    movie.frame.Width = sprite.Width;
        //    movie.frame.Height = sprite.Height;
        //    movie.parentSprite = sprite;
        //    movie.frame.X = sprite.X;
        //    movie.frame.Y = sprite.Y;
        //    movie._ScalingMode = _ScalingMode;
        //    movie.controlStyle = controlStyle;
        //    movie.movieSourceType = movieSourceType;
        //    movie.VideoSourcePath = VideoSourcePath;
        //    movie.VideoName = VideoName;
        //    return movie;
        //}
        #endregion
    }

    /// <summary>
    /// 适配模式
    /// </summary>
    public enum EnumScalingMode
    {
        MPMovieScalingModeNone,
        MPMovieScalingModeAspectFit,
        MPMovieScalingModeAspectFill,
        MPMovieScalingModeFill
    }
    /// <summary>
    /// 播放器类型
    /// </summary>
    public enum EnumControlStyle
    {
        MPMovieControlStyleNone,
        MPMovieControlStyleEmbedded,
        MPMovieControlStyleFullscreen
    }
    /// <summary>
    /// 视频源文件类型
    /// </summary>
    public enum EnumMovieSourceType
    {
        MPMovieSourceTypeUnknown,
        MPMovieSourceTypeFile,
        MPMovieSourceTypeStreaming
    }

    [Serializable]
    public class Frame
    {
        public Frame(Movie mov)
        {
            _owner = mov;
        }
        public Movie _owner;
        public double Width;

        #region 暂时不理解什么意思 -wjj
        //public string widthProperty { get { return (Width * TotalStats.GetCurrentStats().rate).ToString("F0"); } }
        //public double Height;
        //public string heightProperty { get { return (Height * TotalStats.GetCurrentStats().rate).ToString("F0"); } }
        //public double X;
        //public string xProperty { get { return JsonExportHelper.ConvertToAppX(TotalStats.GetCurrentStats().rate, X, _owner.parentSprite.Width, _owner.parentSprite.CorePoint.X, TotalStats.GetCurrentStats().cutWidth).ToString(); } }
        //public double Y;
        //public string yProperty { get { return JsonExportHelper.ConvertToAppY(TotalStats.GetCurrentStats().rate, Y, _owner.parentSprite.Height, _owner.parentSprite.CorePoint.Y).ToString(); } }

        #endregion
    }
}
