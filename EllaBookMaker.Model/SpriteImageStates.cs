namespace Sunflowers
{
    /// <summary>
    /// 切片图片的状态
    /// </summary>
    public enum SpriteImageStates
    {
        none,     //常规状态
        resize,   //改变大小
        rotate,   //改变角度
        core,     //拖拽中心点
        drag,     //拖拽切片
        auxResize,//辅助UI缩放
        auxDrag,   //辅助UI拖拽
        ziMuDrag,   //字幕拖拽
        coreAreaDrag //核心区域拖拽
    }
}