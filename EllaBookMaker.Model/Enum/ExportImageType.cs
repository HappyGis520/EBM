using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaBookMaker.Model
{
    public enum ExportImageType
    {
        普通图片 = 0,           //orgpath,destpath,rate
        普通切片 = 1,           //orgpath,destpath,width,height,rate
        帧动画切片 = 2,         //导出原图、首帧、plist
        骨骼动画切片 = 3,       //tex.png,tex.json,ske.json
        页面背景 = 4,           //剪切 
        直接拷贝 = 5,           //copy
        音频 = 6,               //copy 转码
        空图片 = 7,            //create
        圆角矩形 = 8            //圆角矩形图片

    }
}
