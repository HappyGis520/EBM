namespace EllaBookMaker.Model
{
    /// <summary>
    /// Canvas状态
    /// </summary>
    public enum  EnumCanvasState
    {
        /// <summary>
        /// 切片状态
        /// </summary>
        objState,
        /// <summary>
        /// 绘制切片
        /// </summary>
        sprite,
        /// <summary>
        /// 默认状态 啥也不干
        /// </summary>
        none,
        /// <summary>
        /// 绘制迷宫线
        /// </summary>
        drawMazeLine,
        /// <summary>
        /// 拖动迷宫点
        /// </summary>
        dragMyBorder,
        /// <summary>
        /// 绘制复杂连线线
        /// </summary>
        drawComplexLine
    }
}